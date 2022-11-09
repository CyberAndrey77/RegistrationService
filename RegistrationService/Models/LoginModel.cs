using System.ComponentModel.DataAnnotations;

namespace RegistrationService.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Введите эл. почту")]
        [EmailAddress(ErrorMessage = "Введите коректную эл. почту")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }
    }
}
