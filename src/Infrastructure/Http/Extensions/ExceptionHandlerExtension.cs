namespace Tweetbook.src.Infrastructure.Http.Extensions;

public static class ExceptionHandlerExtension
{
  public static IServiceCollection AddExceptionHandler(
      this IServiceCollection services
    )
  {
    services.AddExceptionHandler<ExceptionHandler>();
    services.AddProblemDetails();

    return services;
  }
}
