using Tweetbook.src.Application.Exceptions;
using Tweetbook.src.Domain.Gateways;

namespace Tweetbook.src.Application.UseCases.Authentication;

public class RefreshTokenUseCase(IJwtGateway jwtGateway)
{
  public async Task<AuthenticationResult> ExecuteAsync(
    string token,
    string refreshToken
  )
  {
    var authResponse = await jwtGateway.RefreshTokenAsync(
      token,
      refreshToken
    );

    if (!authResponse.IsSuccess)
    {
      throw new UnauthorizedException(authResponse.Error);
    }

    return authResponse;
  }
}
