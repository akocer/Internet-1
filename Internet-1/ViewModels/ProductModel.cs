using System.ComponentModel.DataAnnotations;

namespace Internet_1.ViewModels
{
    public class ProductModel
    {
        public int Id { get; set; }




        [Display(Name = "Ürün Adı")]
        [Required(ErrorMessage = "Ürün Adı Giriniz!")]
        public string Name { get; set; }





        [Display(Name = "Ürün Açıklama")]
        [Required(ErrorMessage = "Ürün Açıklama Giriniz!")]
        public string Description { get; set; }





        [Display(Name = "Ürün Fiyatı")]
        [Required(ErrorMessage = "Ürün Fiyatı Giriniz!")]
        public decimal Price { get; set; }


        [Display(Name = "Aktif")]
        public bool IsActive { get; set; }


        [Display(Name = "Kategori")]
        [Required(ErrorMessage = "Kategori Giriniz!")]
        public int CategoryId { get; set; }
    }
}
