using Microsoft.AspNetCore.Mvc;
using Tweetbook.Contracts.Requests;
using Tweetbook.Contracts.Responses;
using Tweetbook.Domain;
using Tweetbook.Repositories;

namespace Tweetbook.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class PostsController(IPostRepository postRepository) : ControllerBase
  {
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var posts = await postRepository.GetPostsAsync();
      var response = posts.Select(ToResponse);

      return Ok(response);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
      var post = await postRepository.GetPostByIdAsync(id);

      if (post == null)
      {
        return NotFound();
      }

      return Ok(ToResponse(post));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
    {
      var post = new Post
      {
        Id = Guid.NewGuid(),
        Name = postRequest.Name
      };

      await postRepository.CreateAsync(post);

      return Created($"/posts/{post.Id}", ToResponse(post));
    }

    private static PostResponse ToResponse(Post post) => new()
    {
      Id = post.Id,
      Name = post.Name
    };
  }
}