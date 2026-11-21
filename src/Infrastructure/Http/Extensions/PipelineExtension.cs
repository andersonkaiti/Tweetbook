namespace Tweetbook.src.Infrastructure.Http.Extensions;

public static class PipelineExtension
{
  public static WebApplication UseApiPipeline(
    this WebApplication application
  )
  {
    if (!application.Environment.IsDevelopment())
    {
      application.UseHsts();
    }

    application.UseSwaggerDocs();

    application.UseHttpsRedirection();
    application.UseAuthentication();
    application.UseAuthorization();
    application.MapControllers();

    application.MapFallback(() => Results.Problem(
      title: "Route not found",
      statusCode: StatusCodes.Status404NotFound
    ));

    application.UseExceptionHandler();

    return application;
  }
}
