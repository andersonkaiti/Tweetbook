using System.ComponentModel.DataAnnotations;

namespace Tweetbook.src.Domain.Entities;

public class Post
{
  [Key]
  public required Guid Id { get; set; }
  public required string Name { get; set; }
}
