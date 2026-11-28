using System.ComponentModel.DataAnnotations;

namespace Tweetbook.src.Infrastructure.Http.Contracts.Requests;

public class RefreshTokenRequest
{
  [Required]
  public required string RefreshToken { get; set; }
  [Required]
  public required string Token { get; set; }
}
