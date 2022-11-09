using System.ComponentModel.DataAnnotations;

namespace RegistrationService.Models
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Введите эл. почту")]
        [EmailAddress(ErrorMessage = "Введите коректную эл. почту")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите пароль"), MinLength(6, ErrorMessage = "Пароль должен быть больше 6 символов")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Введите подтверждающий пароль"), Compare(nameof(Password), ErrorMessage = "Пароли должны совпадать")]
        public string RepeatedPassword { get; set; }
    }
}
