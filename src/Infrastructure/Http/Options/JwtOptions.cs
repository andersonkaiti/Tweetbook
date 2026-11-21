using System.ComponentModel.DataAnnotations;

namespace Tweetbook.src.Infrastructure.Http.Options;

public class JwtOptions
{
  [Required(ErrorMessage = "JWT secret key is missing"), MinLength(16)]
  public required string JwtSecret { get; set; }
}
