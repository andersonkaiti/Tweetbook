namespace Tweetbook.src.Domain.Gateways;

public class AuthenticationResult
{
  public required IEnumerable<string> Errors { get; set; }
  public bool Success { get; set; }
  public string? ErrorMessage { get; set; }
  public string? Token { get; set; }
}


public interface IJwtGateway
{
  Task<AuthenticationResult> RegisterAsync(string email, string password);
  Task<AuthenticationResult> LoginAsync(string email, string password);
}
