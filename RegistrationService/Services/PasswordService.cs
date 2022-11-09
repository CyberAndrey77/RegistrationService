using Microsoft.EntityFrameworkCore;
using RegistrationService.Models;
using System.Security.Cryptography;

namespace RegistrationService.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly ApplicationContext _context;
        public PasswordService(ApplicationContext context)
        {
            _context = context;
        }
        public byte[] CreatePasswordHash(string password)
        {
            byte[] passwordHash;
            using (SHA512 shaM = new SHA512Managed())
            {
                passwordHash = shaM.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            return passwordHash;
        }

        public async Task<string> ResetPasswordAsync(ResetPasswordModel resetPasswordModel)
        {
            if (resetPasswordModel.Password != resetPasswordModel.RepetedPassword)
            {
                return "Пароли не совпадают";
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.VerificationToken == resetPasswordModel.Token);
            if (user == null)
            {
                return "Неверный токен";
            }

            DateTime enteredDate = DateTime.Parse(user.DateExpirationPasswordVerificationToken);

            if (DateTime.Now > enteredDate)
            {
                return "Время для смены пароля истекло, попробуйте еще раз";
            }

            user.Password = CreatePasswordHash(resetPasswordModel.Password);
            user.DateExpirationPasswordVerificationToken = null;
            user.PasswordVerificationToken = null;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return "Пароль обновлен успешно";
        }

        public bool VerifyPassword(string enteredPassword, byte[] password)
        {
            using (SHA512 shaM = new SHA512Managed())
            {
                var computedHash = shaM.ComputeHash(System.Text.Encoding.UTF8.GetBytes(enteredPassword));
                return computedHash.SequenceEqual(password);
            }
        }
    }
}
