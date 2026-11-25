using System.IdentityModel.Tokens.Jwt;

namespace Tweetbook.src.Infrastructure.Http.Extensions;

public static class HttpExtension
{
  public static string GetAuthenticatedUserId(this HttpContext httpContext)
  {
    if (httpContext.User is null)
    {
      return string.Empty;
    }

    return httpContext.User.Claims.Single(claim =>
      claim.Type is JwtRegisteredClaimNames.Sub).Value;
  }
}
