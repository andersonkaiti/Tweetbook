using Microsoft.AspNetCore.Identity;
using Tweetbook.src.Application.Exceptions;
using Tweetbook.src.Domain.Entities;
using Tweetbook.src.Domain.Gateways;

namespace Tweetbook.src.Application.UseCases.Authentication;

public class LoginUseCase(IJwtGateway jwtGateway)
{
  public async Task<AuthenticationResult> ExecuteAsync(User user)
  {
    var authResponse = await jwtGateway.LoginAsync(
        user.Email,
        user.Password
      );

    if (!authResponse.Success)
    {
      throw new BadRequestException(string.Join("; ", authResponse.Errors));
    }

    return authResponse;
  }
}
