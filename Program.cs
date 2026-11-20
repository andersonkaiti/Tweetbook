using Tweetbook.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerDocs(builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
  app.UseHsts();
}

app.UseSwaggerDocs();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapFallback(() => Results.Problem(
  title: "Route not found",
  statusCode: StatusCodes.Status404NotFound
));

app.Run();