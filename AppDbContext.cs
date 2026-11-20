using Microsoft.EntityFrameworkCore;
using Tweetbook.Domain;

namespace Tweetbook;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
  public DbSet<Post> Posts { get; set; }
}