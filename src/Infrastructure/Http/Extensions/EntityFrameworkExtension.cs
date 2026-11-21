using Microsoft.EntityFrameworkCore;
using Tweetbook.src.Infrastructure.Database;

namespace Tweetbook.src.Infrastructure.Http.Extensions;

public static class EntityFrameworkExtension
{
  public static IServiceCollection AddDatabase(
    this IServiceCollection services
  )
  {
    services.AddDbContext<AppDbContext>(options =>
      options.UseNpgsql(Env.ConnectionString, npgsql =>
        npgsql.EnableRetryOnFailure()));

    return services;
  }
}
