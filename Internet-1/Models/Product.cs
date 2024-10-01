using System.ComponentModel.DataAnnotations;

namespace Internet_1.Models
{
    public class Product
    {
        public int Id { get; set; }


        [Display(Name = "Adı")]
        [Required(ErrorMessage = "Adı Giriniz!")]
        [StringLength(20, ErrorMessage = "Ürün Adı 20 Karakter olmalıdır!")]
        public string Name { get; set; }



        [Display(Name = "Açıklama")]
        [Required(ErrorMessage = "Açıklama Giriniz!")]
        public string Description { get; set; }



        [Display(Name = "Fiyat")]
        [Required(ErrorMessage = "Fiyat Giriniz!")]
        public decimal Price { get; set; }



        [Display(Name = "Aktif")]
        public bool IsActive { get; set; }
    }
}
