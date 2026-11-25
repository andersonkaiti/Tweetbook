using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tweetbook.src.Application.Exceptions;

namespace Tweetbook.src.Infrastructure.Http;

public class ExceptionHandler(ILogger<ExceptionHandler> logger)
  : IExceptionHandler
{
  public async ValueTask<bool> TryHandleAsync(
    HttpContext httpContext,
    Exception exception,
    CancellationToken cancellationToken
  )
  {
    logger.LogError(exception, "{Message}", exception.Message);

    if (exception is NotFoundException)
    {
      httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

      await httpContext.Response.WriteAsJsonAsync(
        new ProblemDetails
        {
          Status = StatusCodes.Status404NotFound,
          Title = exception.Message,
        },
        cancellationToken
      );

      return true;
    }

    if (exception is BadRequestException)
    {
      httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

      await httpContext.Response.WriteAsJsonAsync(
        new ProblemDetails
        {
          Status = StatusCodes.Status400BadRequest,
          Title = exception.Message,
        },
        cancellationToken
      );

      return true;
    }

    if (exception is UnauthorizedException)
    {
      httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

      await httpContext.Response.WriteAsJsonAsync(
        new ProblemDetails
        {
          Status = StatusCodes.Status401Unauthorized,
          Title = exception.Message,
        },
        cancellationToken
      );

      return true;
    }

    await httpContext.Response.WriteAsJsonAsync(
      new ProblemDetails
      {
        Status = StatusCodes.Status500InternalServerError,
        Title = "Internal server error"
      },
      cancellationToken
    );

    return true;
  }
}
