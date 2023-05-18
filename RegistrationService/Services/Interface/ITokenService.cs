using RegistrationService.Models;

namespace RegistrationService.Services.Interface
{
    /// <summary>
    /// Интерфейс для создания токенов.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Генерация токена. 
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Сгенирированый токена</returns>
        string GenerateToken(User user);
    }

    public interface IAccessTokenService : ITokenService
    {

    }

    public interface IRefreshTokenService : ITokenService
    {

    }
}
