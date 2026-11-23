using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Tweetbook.src.Infrastructure.Http.Extensions;

public class JwtExtension
{
  public string? Emit(string email, string id)
  {
    var handler = new JwtSecurityTokenHandler();

    var descriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(
      [
        new Claim(JwtRegisteredClaimNames.Sub, email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Email, email),
        new Claim("id", id),
      ]),
      Expires = DateTime.UtcNow.AddHours(2),
      SigningCredentials = new SigningCredentials(
        new SymmetricSecurityKey(
          Encoding.ASCII.GetBytes(Env.JwtSecret)
        ),
        SecurityAlgorithms.HmacSha256Signature
      )
    };

    var token = handler.CreateToken(descriptor);

    return handler.WriteToken(token);
  }
}
