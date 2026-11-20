using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Tweetbook.Options;

namespace Tweetbook.Extensions;

public static class SwaggerExtension
{
  public static IServiceCollection AddSwaggerDocs(
    this IServiceCollection services,
    IConfiguration configuration
  )
  {
    var swaggerSection = configuration.GetSection(nameof(SwaggerOptions));
    var swaggerOptions = new SwaggerOptions();
    swaggerSection.Bind(swaggerOptions);

    services.Configure<SwaggerOptions>(swaggerSection);

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(x =>
    {
      x.SwaggerDoc("v1", new OpenApiInfo
      {
        Title = swaggerOptions.Description,
        Version = "v1"
      });
    });

    return services;
  }

  public static IApplicationBuilder UseSwaggerDocs(this IApplicationBuilder app)
  {
    var swaggerOptions = app.ApplicationServices.
      GetRequiredService<IOptions<SwaggerOptions>>().Value;

    app.UseSwagger(option =>
    {
      option.RouteTemplate = swaggerOptions.JsonRoute;
    });
    app.UseSwaggerUI(option =>
    {
      option.SwaggerEndpoint(
        swaggerOptions.UiEndpoint,
        swaggerOptions.Description
      );
    });

    return app;
  }
}