namespace RegistrationService.Services
{
    /// <summary>
    /// Интерфейс для проверки валидности токена перевыпуска.
    /// </summary>
    public interface IRefreshTokenValidator
    {
        /// <summary>
        /// Является ил токен валидным.
        /// </summary>
        /// <param name="refreshToken">Токен</param>
        /// <returns></returns>
        bool Validate(string refreshToken);
    }
}