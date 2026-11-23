using Microsoft.AspNetCore.Identity;
using Tweetbook.src.Domain.Gateways;
using Tweetbook.src.Infrastructure.Http.Extensions;

namespace Tweetbook.src.Infrastructure.Http.Gateways;

public class JwtGateway(
  UserManager<IdentityUser> userManager,
  JwtExtension jwtExtension
) : IJwtGateway
{
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

    var token = jwtExtension.Emit(email, newUser.Id);

    return new AuthenticationResult
    {
      Errors = [],
      Success = true,
      Token = token
    };
  }
}
