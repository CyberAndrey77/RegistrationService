using Microsoft.EntityFrameworkCore;
using RegistrationService.Models;
using RegistrationService.Services.Interface;
using System.Security.Cryptography;

namespace RegistrationService.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly ApplicationContext _context;
        private readonly IAccessTokenService _accessTokenService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IRefreshTokenValidator _refreshTokenValidator;
        private readonly IPasswordService _passwordService;
        private readonly IEmailService _emailService;

        public AuthorizationService(ApplicationContext context, IAccessTokenService accessTokenService, IRefreshTokenService refreshTokenService, 
            IRefreshTokenValidator refreshTokenValidator, IPasswordService passwordService, IEmailService emailService)
        {
            _context = context;
            _accessTokenService = accessTokenService;
            _refreshTokenService = refreshTokenService;
            _refreshTokenValidator = refreshTokenValidator;
            _passwordService = passwordService;
            _emailService = emailService;
        }

        public async Task<List<string>> Login(LoginModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
            if (user == null)
            {
                throw new ArgumentException("Неверная почта или пароль");
            }

            if (!_passwordService.VerifyPassword(model.Password, user.Password))
            {
                throw new ArgumentException("Неверная почта или пароль");
            }

            //if (!user.IsVerification)
            //{
            //    throw new ArgumentException("Почта не подверждена!");
            //}

            var refreshToken = _refreshTokenService.GenerateToken(user);
            await PutRefreshTokenInDB(user.Id, refreshToken);
            return new List<string>() { _accessTokenService.GenerateToken(user), refreshToken };
        }

        public async Task<List<string>> RefreshToken(RefreshTokenModel model)
        {
            if (!_refreshTokenValidator.Validate(model.RefreshToken))
            {
                throw new ArgumentException("Токен не верный");
            }

            var token = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == model.RefreshToken);

            if (token == null)
            {
                throw new ArgumentException("Токен не верный");
            }

            var user = await _context.Users.FindAsync(token.UserId);

            if (user == null)
            {
                throw new ArgumentException("Пользователь не найден");
            }

            _context.RefreshTokens.Remove(token);

            var refreshToken = _refreshTokenService.GenerateToken(user);
            await PutRefreshTokenInDB(user.Id, refreshToken);

            await _context.SaveChangesAsync();
            return new List<string>() { _accessTokenService.GenerateToken(user), refreshToken };
        }

        public async Task<string> Registration(RegistrationModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
            if (user != null)
            {
                return "Данная эл. почта уже используется";
            }

            string verificationToken = CreateRandomToken();

            user = new User()
            {
                Email = model.Email,
                Name = model.Name,
                Password = _passwordService.CreatePasswordHash(model.Password),
                CreationTime = DateTime.Now.ToString("dd.MM.yyyy"),
                Role = UserRole.Unsigned.ToString(),
                VerificationToken = verificationToken,
                IsVerification = false
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            await _emailService.SendVerificationOnEmailAsync(user.Email, user.VerificationToken);

            return "Успешная регистрация";
        }
        public async Task<string> CreateVerificationPasswordToken(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                return "Пользователь не найден";
            }

            user.PasswordVerificationToken = CreateRandomToken();
            user.DateExpirationPasswordVerificationToken = DateTime.Now.AddDays(1).ToString();
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return "Пройдите по ссылке присланой на электронную почту";
        }



        private async Task PutRefreshTokenInDB(int id, string refreshToken)
        {
            await _context.RefreshTokens.AddAsync(new RefreshToken() { UserId = id, Token = refreshToken });
            await _context.SaveChangesAsync();
        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
    }
}
