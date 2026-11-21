using Microsoft.EntityFrameworkCore;
using Tweetbook.src.Domain.Entities;
using Tweetbook.src.Domain.Repositories;

namespace Tweetbook.src.Infrastructure.Database.Repositories;

public class PostgreSQLPostsRepository(AppDbContext dbContext) : IPostRepository
{
  public async Task<List<Post>> GetPostsAsync()
  {
    return await dbContext.Posts.ToListAsync();
  }

  public async Task<Post?> GetPostByIdAsync(Guid id)
  {
    return await dbContext.Posts.FirstOrDefaultAsync(post => post.Id == id);
  }

  public async Task<bool> CreateAsync(Post post)
  {
    await dbContext.Posts.AddAsync(post);

    var createdRows = await dbContext.SaveChangesAsync();

    return createdRows > 0;
  }

  public async Task<bool> UpdateAsync(Post postToUpdate)
  {
    var updatedRows = await dbContext.Posts
      .Where(post => post.Id == postToUpdate.Id)
      .ExecuteUpdateAsync(setter =>
        setter.SetProperty(property =>
          property.Name, postToUpdate.Name));

    return updatedRows > 0;
  }

  public async Task<bool> DeleteAsync(Guid id)
  {
    var deletedRows = await dbContext.Posts
      .Where(post => post.Id == id)
      .ExecuteDeleteAsync();

    return deletedRows > 0;
  }
}
