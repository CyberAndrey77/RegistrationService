using MailKit.Net.Smtp;
using MimeKit;

namespace RegistrationService.Services
{
    public class EmailService : IEmailService
    {
        private readonly ApplicationContext _context;
        public EmailService(ApplicationContext context)
        {
            _context = context;
        }

        public Task<string> SendLinkForResetPasswordOnEmailAsync(string email, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SendVerificationOnEmailAsync(string email, string token)
        {
            var url = $"https://localhost:7288/api/Authorize/verify?token={token}";
            var text = $"<span>Для подверждения регистрации перейтите по <a href=\"{url}\">ссылке</a></span>";
            await SendMail(email, "Подверждение почты", text);
            return "Сообщение оптравлено на почту";
        }

        private async Task<bool> SendMail(string email, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse("fine.andrey231@yandex.ru"));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync("smtp.yandex.ru", 465, true);
                await smtp.AuthenticateAsync("fine.andrey231@yandex.ru", "aefxgiqdngxcyfpz");
                await smtp.SendAsync(message);

                await smtp.DisconnectAsync(true);
            }
            return true;
        }
    }
}
