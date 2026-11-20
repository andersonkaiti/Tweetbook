using Microsoft.AspNetCore.Mvc;
using Tweetbook.Contracts.Requests;
using Tweetbook.Contracts.Responses;
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

    [HttpPost]
    public IActionResult Create([FromBody] CreatePostRequest postRequest)
    {
      var post = new Post { Id = postRequest.Id };

      if (string.IsNullOrEmpty(post.Id))
      {
        post.Id = Guid.NewGuid().ToString();
      }

      posts.Add(post);

      var response = new PostResponse { Id = post.Id };

      return Created($"/posts/{post.Id}", response);
    }
  }
}