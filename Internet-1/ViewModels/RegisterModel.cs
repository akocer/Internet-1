using System.ComponentModel.DataAnnotations;

namespace Internet_1.ViewModels
{
    public class RegisterModel
    {
        [Display(Name = "Adı Soyadı")]
        [Required(ErrorMessage = "Adı Soyadı Giriniz!")]
        public string FullName { get; set; }



        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Kullanıcı Adı Giriniz!")]
        public string UserName { get; set; }



        [Display(Name = "E-Posta")]
        [Required(ErrorMessage = "E-Posta Giriniz!")]
        public string Email { get; set; }


        [Display(Name = "Parola")]
        [Required(ErrorMessage = "Parola Giriniz!")]
        public string Password { get; set; }


        [Display(Name = "Parola Tekrar")]
        [Required(ErrorMessage = "Parola Tekrar Giriniz!")]
        public string PasswordConfirm { get; set; }


        [Display(Name = "Fotoğraf")]
        public IFormFile PhotoFile { get; set; }
    }
}
