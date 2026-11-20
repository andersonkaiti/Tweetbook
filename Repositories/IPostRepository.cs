using Tweetbook.Domain;

namespace Tweetbook.Repositories;

public interface IPostRepository
{
  Task<List<Post>> GetPostsAsync();
  Task<Post?> GetPostByIdAsync(Guid id);
  Task CreateAsync(Post post);
}