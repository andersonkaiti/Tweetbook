using Tweetbook.src.Application.Exceptions;
using Tweetbook.src.Domain.Entities;
using Tweetbook.src.Domain.Repositories;

namespace Tweetbook.src.Application.UseCases.Posts;

public class GetPostByIdUseCase(IPostRepository postRepository)
{
  public async Task<Post> ExecuteAsync(Guid id)
  {
    var post = await postRepository.GetPostByIdAsync(id)
      ?? throw new NotFoundException("Post not found");

    return post;
  }
}
