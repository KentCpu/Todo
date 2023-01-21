using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class FormModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Некорректная электронная почта")]
        public string email { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Пароль должен иметь длину больше 5 символов")]
        public string password { get; set; }

        public FormModel(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
    }
}
