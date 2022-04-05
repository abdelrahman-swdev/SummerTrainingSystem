using System.ComponentModel.DataAnnotations;

namespace SummerTrainingSystem.Models
{
    public class LoginAsCompanyVM
    {
        public string ReturnUrl { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        [MaxLength(256)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password, ErrorMessage = "Password must be strong")]
        [MinLength(8, ErrorMessage = "Password should at least have 8 characters")]
        public string Password { get; set; }
    }
}
