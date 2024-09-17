using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(40)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "EMail")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must have a minimum of {2} and a maximum of {1} characters.",  MinimumLength = 8)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password doesn't match")]
        public string ConfirmPassword { get; set; }
    }
}
