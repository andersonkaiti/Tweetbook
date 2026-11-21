using Tweetbook.src.Domain.Repositories;
using Tweetbook.src.Infrastructure.Database.Repositories;
using Tweetbook.src.Infrastructure.Http.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.LoadDotEnv();

builder.Services.AddControllers();

builder.Services
  .AddSwaggerDocs(builder.Configuration)
  .AddDatabase()
  .AddJwtAuth()
  .AddScoped<IPostRepository, PostgreSQLPostsRepository>()
  .AddUseCases()
  .AddExceptionHandler();

var app = builder.Build();

app.UseApiPipeline();

app.Run();
