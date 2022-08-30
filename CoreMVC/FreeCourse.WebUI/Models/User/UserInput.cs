using System.ComponentModel.DataAnnotations;


namespace FreeCourse.WebUI.Models.User
{
    public class UserInput
    {
        
        [Display(Name = "Eposta adresiniz")]
        public string Email { get; set; }

        
        [Display(Name = "Şifreniz")]
        public string Password { get; set; }

        [Display(Name = "Beni Hatırla")]
        public bool IsRemember { get; set; }

    }
}
