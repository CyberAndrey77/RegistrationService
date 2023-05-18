namespace RegistrationService.Services.Interface
{
    public interface IVerifyService
    {
        Task<string> VerifyEmail(string token);
    }
}
