using Microsoft.IdentityModel.Tokens;
using MyBlog.Web.Api.Models;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyBlog.Web.Api.Services
{
    public class TokenService(IConfiguration configuration) : ITokenService
    {
        public string CreateToken(WebApiUser user)
        {
            var token = CreateJwtToken(CreateClaims(user), CreateSigningCredentials()
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        private JwtSecurityToken CreateJwtToken(IEnumerable<Claim> claims, SigningCredentials credentials) =>
            new(
                configuration.GetValue<string>("Jwt:Issuer"),
                configuration.GetValue<string>("Jwt:Audience"),
                claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(configuration["Authentication:ExpirationInMinutes"]!)),
                signingCredentials: credentials
            );

        private static List<Claim> CreateClaims(WebApiUser user)
        {
            var claims = new List<Claim>
                {
                    new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                    new(ClaimTypes.NameIdentifier, user.Username),
                    new(ClaimTypes.Name, user.Username)
                };
            return claims;
        }

        private SigningCredentials CreateSigningCredentials()
        {
            return new SigningCredentials(
            new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:Key")!)
                ),
                SecurityAlgorithms.HmacSha256
            );
        }
    }
}