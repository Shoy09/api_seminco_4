using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Seminco.Application.Auth;
using Seminco.Domain.Users;
using Seminco.Infrastructure.Configuration;

namespace Seminco.Infrastructure.Auth;

public sealed class JwtTokenIssuer(IOptions<JwtOptions> options) : IJwtTokenIssuer
{
    public string Issue(User user)
    {
        var jwt = options.Value;
        var claims = new[]
        {
            new Claim("id", user.Id.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim("codigo_dni", user.CodigoDni),
            new Claim("apellidos", user.Apellidos),
            new Claim("nombres", user.Nombres)
        };

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Secret)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(jwt.Issuer, jwt.Audience, claims,
            expires: DateTime.UtcNow.AddHours(jwt.ExpirationHours), signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
