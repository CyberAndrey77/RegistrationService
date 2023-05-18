namespace RegistrationService.Services.Interface
{
    public interface ISendMessageService
    {
        Task<string> SendMessage(string message);
    }
}
