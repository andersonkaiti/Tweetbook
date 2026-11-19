using Microsoft.AspNetCore.Mvc;
using Tweetbook.Domain;

namespace Tweetbook.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class PostsController : ControllerBase
  {
    private readonly List<Post> posts = [];
    private readonly int postsQuantity = 5;

    public PostsController()
    {
      for (var i = 0; i < postsQuantity; i++)
      {
        posts.Add(new Post
        {
          Id = Guid.NewGuid().ToString()
        });
      }
    }

    [HttpGet]
    public IActionResult GetAll()
    {
      return Ok(posts);
    }
  }
}