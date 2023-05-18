using RegistrationService.Models;

namespace RegistrationService.Services.Interface
{
    public interface IPasswordService
    {
        byte[] CreatePasswordHash(string password);
        bool VerifyPassword(string enteredPassword, byte[] password);
        Task<string> ResetPasswordAsync(ResetPasswordModel resetPasswordModel);
    }
}
