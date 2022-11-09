namespace RegistrationService.Services
{
    public interface IVerifyService
    {
        Task<string> VerifyEmail(string token);
    }
}
