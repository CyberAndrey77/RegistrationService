using RegistrationService.Models;

namespace RegistrationService.Services
{
    public interface IAuthorizationService
    {
        Task<string> Registration(RegistrationModel model);

        Task<List<string>> Login(LoginModel model);

        Task<List<string>> RefreshToken(RefreshTokenModel model);

        Task<string> CreateVerificationPasswordToken(string email);
    }
}
