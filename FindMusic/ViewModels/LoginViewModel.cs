using System.ComponentModel.DataAnnotations;

namespace FindMusic.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username/Email")]
        public string Identifier { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}