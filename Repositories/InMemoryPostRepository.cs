using Tweetbook.Domain;

namespace Tweetbook.Repositories;

public class InMemoryPostRepository : IPostRepository
{
  private const int PostsQuantity = 5;

  private readonly List<Post> posts = [];
  private readonly Lock sync = new();

  public InMemoryPostRepository()
  {
    for (var i = 0; i < PostsQuantity; i++)
    {
      posts.Add(new Post
      {
        Id = Guid.NewGuid(),
        Name = $"Post name {i}"
      });
    }
  }

  public async Task<List<Post>> GetPostsAsync()
  {
    lock (sync)
    {
      return posts;
    }
  }

  public async Task<Post?> GetPostByIdAsync(Guid id)
  {
    lock (sync)
    {
      return posts.FirstOrDefault(post => post.Id == id);
    }
  }

  public async Task<bool> CreateAsync(Post post)
  {
    lock (sync)
    {
      posts.Add(post);

      return true;
    }
  }

  public async Task<bool> UpdateAsync(Post postToUpdate)
  {
    var postExists = await GetPostByIdAsync(postToUpdate.Id) != null;

    if (!postExists)
    {
      return false;
    }

    var postIndex = posts.FindIndex(post => post.Id == postToUpdate.Id);

    posts[postIndex] = postToUpdate;

    return true;
  }

  public async Task<bool> DeleteAsync(Guid id)
  {
    var postExists = await GetPostByIdAsync(id);

    if (postExists == null)
    {
      return false;
    }

    posts.Remove(postExists);

    return true;
  }
}