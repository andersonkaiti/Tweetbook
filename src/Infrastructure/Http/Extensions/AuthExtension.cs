namespace Tweetbook.src.Infrastructure.Http.Extensions;

public static class AuthExtension
{
  public static IServiceCollection AddJwtAuth(
    this IServiceCollection services
  )
  {
    services.AddAuthorization();

    return services;
  }
}
