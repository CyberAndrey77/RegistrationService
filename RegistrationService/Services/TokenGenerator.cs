using Microsoft.IdentityModel.Tokens;
using RegistrationService.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RegistrationService.Services
{
    public class TokenGenerator : ITokenGenerator
    {
        public string GenerateToken(string secretKey, string issuer, string audience, double expires, IEnumerable<Claim>? claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken securityToken = new
            (
                issuer,
                audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(expires),
                credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public string GenerateToken(string secretKey, string issuer, string audience, double expires)
        {
            return GenerateToken(secretKey, issuer, audience, expires, null);
        }
    }
}
