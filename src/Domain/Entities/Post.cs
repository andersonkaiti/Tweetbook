using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Tweetbook.src.Domain.Entities;

public class Post
{
  [Key]
  public required Guid Id { get; set; }
  public required string Name { get; set; }
  public string? AuthorId { get; set; }
  [ForeignKey(nameof(AuthorId))]
  public IdentityUser? Author { get; set; }
}
