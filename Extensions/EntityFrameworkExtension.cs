using Microsoft.EntityFrameworkCore;

namespace Tweetbook.Extensions;

public static class EntityFrameworkExtension
{
  public static IServiceCollection AddDatabase(
    this IServiceCollection services,
    IConfiguration configuration
  )
  {
    var connectionString = configuration.GetConnectionString("Default")
      ?? throw new InvalidOperationException(
        "Connection string 'Default' not found"
      );

    services.AddDbContext<AppDbContext>(options =>
      options.UseNpgsql(connectionString, npgsql =>
        npgsql.EnableRetryOnFailure()));

    return services;
  }
}