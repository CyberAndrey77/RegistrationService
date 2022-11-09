using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistrationService.Models
{
    public class User
    {
        /// <summary>
        /// Id пользователя.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Email полльзователя.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public byte[] Password { get; set; }

        /// <summary>
        /// Дата регистрации.
        /// </summary>
        public string CreationTime { get; set; }

        /// <summary>
        /// Роль пользователя.
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Токен отправляющийся на почту пользователя для подверждения.
        /// </summary>
        public string? VerificationToken { get; set; }

        /// <summary>
        /// Подверждена ли почта
        /// </summary>
        public bool IsVerification { get; set; }

        /// <summary>
        /// Токен для смены пароля.
        /// </summary>
        public string? PasswordVerificationToken { get; set; }

        /// <summary>
        /// Дата истечения токена для востановления пароля.
        /// </summary>
        public string? DateExpirationPasswordVerificationToken { get; set; }
    }
}
