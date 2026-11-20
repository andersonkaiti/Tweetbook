using System.ComponentModel.DataAnnotations;

namespace Tweetbook.Domain;

public class Post
{
  [Key]
  public required Guid Id { get; set; }
  public required string Name { get; set; }
}