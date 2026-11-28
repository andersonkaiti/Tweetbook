using System.Diagnostics.CodeAnalysis;

namespace Tweetbook.src.Domain.Gateways;

public record class AuthenticationResult(
  bool IsSuccess,
  string? Token = null,
  string? Error = null
)
{
  [MemberNotNullWhen(true, nameof(Token))]
  [MemberNotNullWhen(false, nameof(Error))]
  public bool IsSuccess { get; init; } = IsSuccess;

  public static AuthenticationResult Success(string token)
    => new(true, token, null);
  public static AuthenticationResult Failed(string error)
    => new(false, null, error);
}
