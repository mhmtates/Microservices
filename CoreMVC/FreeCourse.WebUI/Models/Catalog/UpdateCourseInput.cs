using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FreeCourse.WebUI.Models.Catalog
{
    public class UpdateCourseInput
    {
      
        public string Id { get; set; }

        [Required(ErrorMessage = "Kurs adı boş bırakılamaz.")]
        [Display(Name = "Kurs Adı")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Açıklama boş bırakılamaz.")]
        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Fiyat boş bırakılamaz.")]
        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }


        public string UserId { get; set; }

        public string Picture { get; set; }
        public FeatureViewModel Feature { get; set; }


        [Required(ErrorMessage = "Kategori boş bırakılamaz.")]
        [Display(Name = "Kategori")]
        public string CategoryId { get; set; }

        [Display(Name = "Resim")]
        public IFormFile ImageFormFile { get; set;}
    }
}

