using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Tweetbook.src.Domain.Gateways;
using Tweetbook.src.Infrastructure.Http.Gateways;

namespace Tweetbook.src.Infrastructure.Http.Extensions;

public static class AuthExtension
{
  public static IServiceCollection AddJwtAuth(
    this IServiceCollection services
  )
  {
    var tokenValidationParameters = new TokenValidationParameters
    {
      ValidateIssuer = false,
      ValidateAudience = false,
      ValidateLifetime = true,
      ValidateIssuerSigningKey = true,
      IssuerSigningKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(Env.JwtSecret)
      ),
      ValidAlgorithms = [SecurityAlgorithms.HmacSha256]
    };

    services.AddSingleton(tokenValidationParameters);

    services
      .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
      {
        options.MapInboundClaims = false;
        options.TokenValidationParameters = tokenValidationParameters;
      });

    services.AddAuthorization();

    services.AddScoped<IJwtGateway, JwtGateway>();

    return services;
  }
}
