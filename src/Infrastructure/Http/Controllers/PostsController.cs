using Microsoft.AspNetCore.Mvc;
using Tweetbook.src.Infrastructure.Http.Contracts.Requests;
using Tweetbook.src.Infrastructure.Http.Contracts.Responses;
using Tweetbook.src.Domain.Entities;
using Tweetbook.src.Application.UseCases.Posts;

namespace Tweetbook.src.Infrastructure.Http.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class PostsController : ControllerBase
  {
    [HttpGet]
    public async Task<IActionResult> GetAll([FromServices] GetPostsUseCase useCase)
    {
      var posts = await useCase.ExecuteAsync();

      var response = posts.Select(ToResponse);

      return Ok(response);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById([FromServices] GetPostByIdUseCase useCase, [FromRoute] Guid id)
    {
      var post = await useCase.ExecuteAsync(id);

      return Ok(ToResponse(post));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromServices] CreatePostUseCase useCase, [FromBody] CreatePostRequest postRequest)
    {
      var post = new Post
      {
        Id = Guid.NewGuid(),
        Name = postRequest.Name
      };

      await useCase.ExecuteAsync(post);

      return Created($"/posts/{post.Id}", ToResponse(post));
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Update(
      [FromServices] UpdatePostUseCase useCase,
      [FromRoute] Guid id,
      [FromBody] UpdatePostRequest request)
    {
      var post = new Post
      {
        Id = id,
        Name = request.Name
      };

      await useCase.ExecuteAsync(post);

      return Ok(post);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete([FromServices] DeletePostUseCase useCase, [FromRoute] Guid id)
    {
      await useCase.ExecuteAsync(id);

      return NoContent();
    }

    private static PostResponse ToResponse(Post post) => new()
    {
      Id = post.Id,
      Name = post.Name
    };
  }
}
