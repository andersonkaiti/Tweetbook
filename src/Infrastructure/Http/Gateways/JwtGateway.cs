using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Tweetbook.src.Domain.Entities;
using Tweetbook.src.Domain.Gateways;
using Tweetbook.src.Infrastructure.Database;
using Tweetbook.src.Infrastructure.Http.Extensions;

namespace Tweetbook.src.Infrastructure.Http.Gateways;

public class JwtGateway(
  UserManager<IdentityUser> userManager,
  TokenValidationParameters tokenValidationParameters,
  AppDbContext dbContext
) : IJwtGateway
{
  private readonly JwtSecurityTokenHandler handler = new();

  public SecurityToken Emit(string email, string id)
  {
    var descriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(
      [
        new Claim(JwtRegisteredClaimNames.Sub, id),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Email, email),
      ]),
      Expires = DateTime.UtcNow.AddSeconds(45),
      SigningCredentials = new SigningCredentials(
        new SymmetricSecurityKey(
          Encoding.ASCII.GetBytes(Env.JwtSecret)
        ),
        SecurityAlgorithms.HmacSha256Signature
      )
    };

    var token = handler.CreateToken(descriptor);

    return token;
  }

  public ClaimsPrincipal? Validate(string token)
  {
    try
    {
      return handler.ValidateToken(
         token,
         tokenValidationParameters,
         out _
       );
    }
    catch
    {
      return null;
    }
  }

  private async Task<AuthenticationResult> GenerateTokensAsync(
    string email,
    string id
  )
  {
    var token = Emit(email, id);

    var refreshToken = new RefreshToken
    {
      Token = Guid.NewGuid().ToString(),
      JwtId = token.Id,
      UserId = id,
      CreationDate = DateTime.UtcNow,
      ExpireDate = DateTime.UtcNow.AddDays(7),
    };

    dbContext.RefreshTokens.Add(refreshToken);
    await dbContext.SaveChangesAsync();

    return AuthenticationResult.Success(handler.WriteToken(token), refreshToken.Token);
  }

  public async Task<AuthenticationResult> RegisterAsync(
    string email,
    string password
  )
  {
    var userAlreadyExists = await userManager.FindByEmailAsync(email);

    if (userAlreadyExists is not null)
    {
      return AuthenticationResult.Failed(
        "An user with this email address already exists"
      );
    }

    var newUser = new IdentityUser
    {
      Email = email,
      UserName = email
    };

    var wasCreated = await userManager.CreateAsync(newUser, password);

    if (!wasCreated.Succeeded)
    {
      return AuthenticationResult.Failed(
        string.Join("; ", wasCreated.Errors.Select(error => error.Description)
      ));
    }

    return await GenerateTokensAsync(email, newUser.Id);
  }

  public async Task<AuthenticationResult> LoginAsync(
    string email,
    string password
  )
  {
    var user = await userManager.FindByEmailAsync(email);

    if (user is null)
    {
      return AuthenticationResult.Failed("Invalid credentials");
    }

    var userHasValidPassword = await userManager.CheckPasswordAsync(
      user,
      password
    );

    if (!userHasValidPassword)
    {
      return AuthenticationResult.Failed("Invalid credentials");
    }

    return await GenerateTokensAsync(email, user.Id);
  }

  public async Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
  {
    var validatedToken = Validate(token);

    if (validatedToken is null)
    {
      return AuthenticationResult.Failed("Invalid token");
    }

    var jti = validatedToken.Claims.Single(claim =>
      claim.Type == JwtRegisteredClaimNames.Jti
    );

    var storedRefreshToken = await dbContext.RefreshTokens
      .SingleOrDefaultAsync(value => value.Token == refreshToken);

    if (storedRefreshToken is null)
    {
      return AuthenticationResult.Failed("Invalid token");
    }

    if (DateTime.UtcNow > storedRefreshToken.ExpireDate)
    {
      return AuthenticationResult.Failed("Invalid token");
    }

    if (
      storedRefreshToken.Used
      || storedRefreshToken.Invalidated
      || storedRefreshToken.JwtId != jti.Value
    )
    {
      return AuthenticationResult.Failed("Invalid token");
    }

    storedRefreshToken.Used = true;
    dbContext.Update(storedRefreshToken);
    await dbContext.SaveChangesAsync();

    var user = await userManager.FindByIdAsync(storedRefreshToken.UserId);

    if (user is null)
    {
      return AuthenticationResult.Failed("Invalid token");
    }

    return await GenerateTokensAsync(user.Email!, user.Id);
  }
}
