using Tweetbook.src.Domain.Entities;
using Tweetbook.src.Domain.Repositories;

namespace Tweetbook.src.Application.UseCases.Posts;

public class GetPostsUseCase(IPostRepository postRepository)
{
  public async Task<List<Post>> ExecuteAsync()
  {
    return await postRepository.GetPostsAsync();
  }
}
