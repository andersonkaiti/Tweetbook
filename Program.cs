using Microsoft.OpenApi;
using Tweetbook.Options;

var builder = WebApplication.CreateBuilder(args);

var swaggerOptions = new SwaggerOptions();
builder.Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
  x.SwaggerDoc("v1", new OpenApiInfo
  {
    Title = swaggerOptions.Description,
    Version = "v1"
  });
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
  app.UseHsts();
}

app.UseSwagger(option =>
{
  option.RouteTemplate = swaggerOptions.JsonRoute;
});
app.UseSwaggerUI(option =>
{
  option.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description);
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapFallback(() => Results.Problem(
  title: "Route not found",
  statusCode: StatusCodes.Status404NotFound
));

app.Run();