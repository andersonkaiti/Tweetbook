using Tweetbook.src.Application.Exceptions;
using Tweetbook.src.Domain.Repositories;

namespace Tweetbook.src.Application.UseCases.Posts;

public class DeletePostUseCase(IPostRepository postRepository)
{
  public async Task<bool> ExecuteAsync(Guid id, string userId)
  {
    var userOwnsPost = await postRepository.UserOwnsPostAsync(id, userId);

    if (!userOwnsPost)
    {
      throw new UnauthorizedException("You don't own this post");
    }

    var wasDeleted = await postRepository.DeleteAsync(id);

    if (!wasDeleted)
    {
      throw new NotFoundException("Post not found");
    }

    return wasDeleted;
  }
}
