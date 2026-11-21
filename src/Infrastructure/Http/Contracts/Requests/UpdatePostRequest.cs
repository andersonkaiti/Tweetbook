using System.ComponentModel.DataAnnotations;

namespace Tweetbook.src.Infrastructure.Http.Contracts.Requests;

public class UpdatePostRequest
{
  [Required(ErrorMessage = "Name is required")]
  public string Name { get; set; } = string.Empty;
}
