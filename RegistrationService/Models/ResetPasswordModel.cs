using System.ComponentModel.DataAnnotations;

namespace RegistrationService.Models
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Нет токена")]
        public string Token { get; set; }

        [Required, MinLength(6, ErrorMessage = "Пароль должен быть больше 6 символов")]
        public string Password { get; set; }

        [Required, Compare(nameof(Password), ErrorMessage = "Пароли должны совпадать")]
        public string RepetedPassword { get; set; }
    }
}
