using RegistrationService.Models;
using System.Security.Claims;

namespace RegistrationService.Services
{
    public class AccessTokenService : IAccessTokenService
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly JwtSettings _jwtSettings;

        public AccessTokenService(ITokenGenerator tokenGenerator, JwtSettings jwtSettings)
        {
            _tokenGenerator = tokenGenerator;
            _jwtSettings = jwtSettings;
        }

        public string GenerateToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role),
            };
            return _tokenGenerator.GenerateToken(_jwtSettings.AccessTokenSecret, _jwtSettings.Issuer, _jwtSettings.Audience,
                _jwtSettings.AccessTokenExpirationMinutes, claims);
        }
    }
}
