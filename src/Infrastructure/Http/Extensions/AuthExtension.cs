using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Tweetbook.src.Infrastructure.Http.Extensions;

public static class AuthExtension
{
  public static IServiceCollection AddJwtAuth(
    this IServiceCollection services
  )
  {
    services
      .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
      {
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuer = false,
          ValidateAudience = false,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(Env.JwtSecret))
        };
      });


    services.AddAuthorization();

    return services;
  }
}
