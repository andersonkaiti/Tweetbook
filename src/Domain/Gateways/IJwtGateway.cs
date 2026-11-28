namespace Tweetbook.src.Domain.Gateways;

public interface IJwtGateway
{
  Task<AuthenticationResult> RegisterAsync(string email, string password);
  Task<AuthenticationResult> LoginAsync(string email, string password);
  Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
}
