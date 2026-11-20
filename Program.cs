using Tweetbook.Extensions;
using Tweetbook.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.LoadDotEnv();

builder.Services.AddControllers();
builder.Services.AddSwaggerDocs(builder.Configuration);

builder.Services.AddDatabase(builder.Configuration);

builder.Services.AddScoped<IPostRepository, PostgreSQLPostsRepository>();

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