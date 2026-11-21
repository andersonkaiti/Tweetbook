using Microsoft.EntityFrameworkCore;
using Tweetbook.src.Domain.Entities;

namespace Tweetbook.src.Infrastructure.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
  public DbSet<Post> Posts { get; set; }
}
