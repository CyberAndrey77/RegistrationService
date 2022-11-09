namespace RegistrationService.Services
{
    public interface IEmailService
    {
        Task<string> SendVerificationOnEmailAsync(string email, string token);
        Task<string> SendLinkForResetPasswordOnEmailAsync(string email, string token);
    }
}
