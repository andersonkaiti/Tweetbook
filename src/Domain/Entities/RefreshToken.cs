using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Tweetbook.src.Domain.Entities;

public class RefreshToken
{
  [Key]
  public required string Token { get; set; }
  public required string JwtId { get; set; }
  public DateTime CreationDate { get; set; }
  public DateTime ExpireDate { get; set; }
  public bool Used { get; set; }
  public bool Invalidated { get; set; }
  public required string UserId { get; set; }
  [ForeignKey(nameof(UserId))]
  public IdentityUser? User { get; set; }
}
