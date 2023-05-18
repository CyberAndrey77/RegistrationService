namespace RegistrationService.Services.Interface
{
    public interface IEmailService
    {
        Task<string> SendVerificationOnEmailAsync(string email, string token);
        Task<string> SendLinkForResetPasswordOnEmailAsync(string email, string token);
    }
}
