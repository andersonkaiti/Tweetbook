using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tweetbook.src.Application.Exceptions;

namespace Tweetbook.src.Infrastructure.Http;

public class ExceptionHandler : IExceptionHandler
{
  public async ValueTask<bool> TryHandleAsync(
    HttpContext httpContext,
    Exception exception,
    CancellationToken cancellationToken
  )
  {
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
