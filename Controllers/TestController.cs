using Microsoft.AspNetCore.Mvc;

namespace Tweetbook.Controllers
{
  [ApiController]
  public class TestController : ControllerBase
  {
    [HttpGet("api/user")]
    public IActionResult Get()
    {
      return Ok(new
      {
        name = "Anderson"
      });
    }
  }
}