using System.ComponentModel.DataAnnotations;

namespace Tweetbook.src.Infrastructure.Http.Contracts.Requests;

public class LoginUserRequest
{
  [EmailAddress(ErrorMessage = "A valid email address is required")]
  public required string Email { get; set; }
  [Required(ErrorMessage = "Password is required")]
  public required string Password { get; set; }
}
