using Tweetbook.src.Domain.Entities;

namespace Tweetbook.src.Domain.Repositories;

public interface IPostRepository
{
  Task<List<Post>> GetPostsAsync();
  Task<Post?> GetPostByIdAsync(Guid id);
  Task<bool> CreateAsync(Post post);
  Task<bool> UpdateAsync(Post postToUpdate);
  Task<bool> DeleteAsync(Guid id);
  Task<bool> UserOwnsPostAsync(Guid postId, string userId);
}
