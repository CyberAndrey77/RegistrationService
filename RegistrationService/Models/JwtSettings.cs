namespace RegistrationService.Models
{
    public class JwtSettings
    {
        /// <summary>
        /// Серкретное слово для токкена доступа.
        /// </summary>
        public string AccessTokenSecret { get; set; }

        /// <summary>
        /// Сектретное слово для токкена перевысупска.
        /// </summary>
        public string RefreshTokenSecret { get; set; }

        /// <summary>
        /// Время действия токкена доступа.
        /// </summary>
        public double AccessTokenExpirationMinutes { get; set; }

        /// <summary>
        /// Время действия токкена перевыпуска.
        /// </summary>
        public double RefreshTokenExpirationMinutes { get; set; }

        /// <summary>
        /// Кто является создателем токкена.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Кто является получателем токкена.
        /// </summary>
        public string Audience { get; set; }
    }
}
