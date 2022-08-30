using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace FreeCourse.WebUI.Models.Catalog
{
    public class CreateCourseInput
    {
       
        
        [Display(Name = "Kurs Adı")]
        public string Name { get; set; }

        
        [Display(Name = "Açıklama")]
        public string Description { get; set; }

       
        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }

       
        public string Picture { get; set; }

        public string UserId { get; set; }

        public FeatureViewModel Feature { get; set; }


       
        [Display(Name = "Kategori")]
        public string CategoryId { get; set; }
        
        [Display(Name = "Resim")]
        public IFormFile ImageFormFile { get;set; }
    }
}
