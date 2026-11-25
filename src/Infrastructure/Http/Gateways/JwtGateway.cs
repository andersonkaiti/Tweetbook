using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Tweetbook.src.Domain.Gateways;
using Tweetbook.src.Infrastructure.Http.Extensions;

namespace Tweetbook.src.Infrastructure.Http.Gateways;

public class JwtGateway(UserManager<IdentityUser> userManager) : IJwtGateway
{
  public string? Emit(string email, string id)
  {
    var handler = new JwtSecurityTokenHandler();

    var descriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(
      [
        new Claim(JwtRegisteredClaimNames.Sub, id),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Email, email),
      ]),
      Expires = DateTime.UtcNow.AddHours(2),
      SigningCredentials = new SigningCredentials(
        new SymmetricSecurityKey(
          Encoding.ASCII.GetBytes(Env.JwtSecret)
        ),
        SecurityAlgorithms.HmacSha256Signature
      )
    };

    var token = handler.CreateToken(descriptor);

    return handler.WriteToken(token);
  }

  public async Task<AuthenticationResult> RegisterAsync(
    string email,
    string password
  )
  {
    var userAlreadyExists = await userManager.FindByEmailAsync(email);

    if (userAlreadyExists is not null)
    {
      return new AuthenticationResult
      {
        Errors = ["An user with this email address already exists"]
      };
    }

    var newUser = new IdentityUser
    {
      Email = email,
      UserName = email
    };

    var createdUser = await userManager.CreateAsync(newUser, password);

    if (!createdUser.Succeeded)
    {
      return new AuthenticationResult
      {
        Errors = createdUser.Errors.Select(error => error.Description)
      };
    }

    var token = Emit(email, newUser.Id);

    return new AuthenticationResult
    {
      Errors = [],
      Success = true,
      Token = token
    };
  }

  public async Task<AuthenticationResult> LoginAsync(
    string email,
    string password
  )
  {
    var user = await userManager.FindByEmailAsync(email);

    if (user is null)
    {
      return new AuthenticationResult
      {
        Errors = ["Invalid credentials"]
      };
    }

    var userHasValidPassword = await userManager
      .CheckPasswordAsync(user, password);

    if (!userHasValidPassword)
    {
      return new AuthenticationResult
      {
        Errors = ["Invalid credentials"]
      };
    }

    var token = Emit(email, user.Id);

    return new AuthenticationResult
    {
      Errors = [],
      Success = true,
      Token = token
    };
  }
}
