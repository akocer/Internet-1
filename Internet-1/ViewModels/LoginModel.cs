using System.ComponentModel.DataAnnotations;

namespace Internet_1.ViewModels
{
    public class LoginModel
    {
        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Kullanıcı Adı Giriniz!")]
        public string UserName { get; set; }

        [Display(Name = "Parola")]
        [Required(ErrorMessage = "Parola Giriniz!")]
        public string Password { get; set; }

        [Display(Name = "Beni Hatırla")]
        public bool KeepMe { get; set; }
    }
}
