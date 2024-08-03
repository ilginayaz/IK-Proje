using System.ComponentModel.DataAnnotations;

namespace IKProject.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Kullanıcı adı gereklidir!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre gereklidir!")]
        public string Password { get; set; }

   
    }
}
