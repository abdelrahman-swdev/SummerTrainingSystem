using System.ComponentModel.DataAnnotations;

namespace SummerTrainingSystem.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage = "University id is required")]
        [Display(Name = "University Id")]
        public int UniversityId { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password, ErrorMessage = "Password must be strong")]
        [MinLength(8, ErrorMessage = "Password should at least have 8 characters")]
        public string Password { get; set; }
    }
}
