using Microsoft.AspNetCore.Identity;
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

    services
      .AddIdentityCore<IdentityUser>(options =>
      {
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.User.RequireUniqueEmail = true;
      })
      .AddEntityFrameworkStores<AppDbContext>();

    return services;
  }
}
