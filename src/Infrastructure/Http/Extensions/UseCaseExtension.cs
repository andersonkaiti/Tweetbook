using Tweetbook.src.Application.UseCases.Posts;

namespace Tweetbook.src.Infrastructure.Http.Extensions;

public static class UseCaseExtension
{
  public static IServiceCollection AddUseCases(
    this IServiceCollection services
  )
  {
    services.AddScoped<GetPostsUseCase>();
    services.AddScoped<GetPostByIdUseCase>();
    services.AddScoped<CreatePostUseCase>();
    services.AddScoped<UpdatePostUseCase>();
    services.AddScoped<DeletePostUseCase>();

    return services;
  }
}
