using System.ComponentModel.DataAnnotations;

namespace Tweetbook.src.Infrastructure.Http.Contracts.Requests;

public class CreatePostRequest
{
  [Required(ErrorMessage = "Name is required")]
  public string Name { get; set; } = string.Empty;
}
