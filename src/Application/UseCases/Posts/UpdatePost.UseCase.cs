using Tweetbook.src.Application.Exceptions;
using Tweetbook.src.Domain.Entities;
using Tweetbook.src.Domain.Repositories;

namespace Tweetbook.src.Application.UseCases.Posts;

public class UpdatePostUseCase(IPostRepository postRepository)
{
  public async Task<bool> ExecuteAsync(Post post)
  {
    var wasPostUpdated = await postRepository.UpdateAsync(post);

    if (!wasPostUpdated)
    {
      throw new NotFoundException("Post not found");
    }

    return wasPostUpdated;
  }
}
