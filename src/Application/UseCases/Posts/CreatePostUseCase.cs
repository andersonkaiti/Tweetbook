using Tweetbook.src.Domain.Entities;
using Tweetbook.src.Domain.Repositories;

namespace Tweetbook.src.Application.UseCases.Posts;

public class CreatePostUseCase(IPostRepository postRepository)
{
  public async Task ExecuteAsync(Post post)
  {
    await postRepository.CreateAsync(post);
  }
}
