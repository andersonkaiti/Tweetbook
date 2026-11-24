using Microsoft.AspNetCore.Mvc;
using Tweetbook.src.Application.UseCases.Authentication;
using Tweetbook.src.Domain.Entities;
using Tweetbook.src.Infrastructure.Http.Contracts.Requests;

namespace Tweetbook.src.Infrastructure.Http.Controllers
{
  [Route("/[controller]/[action]")]
  [ApiController]
  public class IdentityController : ControllerBase
  {
    [HttpPost]
    public async Task<IActionResult> Register(
      [FromServices] RegisterUseCase useCase,
      [FromBody] RegisterUserRequest request
    )
    {
      var user = new User
      {
        Email = request.Email,
        Password = request.Password
      };

      var authResponse = await useCase.ExecuteAsync(user);

      return Ok(authResponse.Token);
    }

    [HttpPost]
    public async Task<IActionResult> Login(
      [FromServices] LoginUseCase useCase,
      [FromBody] LoginUserRequest request
    )
    {
      var user = new User
      {
        Email = request.Email,
        Password = request.Password
      };

      var authResponse = await useCase.ExecuteAsync(user);

      return Ok(authResponse.Token);
    }
  }
}
