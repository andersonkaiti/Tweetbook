using Microsoft.AspNetCore.Identity;
using Tweetbook.src.Application.Exceptions;
using Tweetbook.src.Domain.Entities;
using Tweetbook.src.Domain.Gateways;

namespace Tweetbook.src.Application.UseCases.Authentication;

public class RegisterUseCase(IJwtGateway jwtGateway)
{
  public async Task<AuthenticationResult> ExecuteAsync(User user)
  {
    var authResponse = await jwtGateway.RegisterAsync(
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
