using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tweetbook.src.Domain.Entities;

namespace Tweetbook.src.Infrastructure.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options)
  : IdentityDbContext<IdentityUser>(options)
{
  public DbSet<Post> Posts { get; set; }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);
  }
}
