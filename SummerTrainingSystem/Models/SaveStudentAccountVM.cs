using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SummerTrainingSystem.Models
{
    public class SaveStudentAccountVM
    {
        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        [MaxLength(256)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        [MaxLength(256)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [Display(Name = "Phone Number")]
        [MaxLength(256)]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        [MaxLength(256)]
        [Remote(action: "IsEmailInUse", controller: "Account")]
        public string Email { get; set; }

        [Required(ErrorMessage = "University Id is required")]
        [Display(Name = "University Id")]
        [Remote(action: "IsUniversityIdInUse", controller: "Account")]
        public int? UniversityID { get; set; }

        [Required(ErrorMessage = "GPA is required"), Display(Name ="GPA")]
        public float? Gpa { get; set; }

        [Required(ErrorMessage = "Level is required")]
        public byte? Level { get; set; }

        [Required(ErrorMessage = "Department is required")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password, ErrorMessage = "Password must be strong")]
        [MinLength(8, ErrorMessage = "Password should at least have 8 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password, ErrorMessage = "Password doesn't match")]
        [Compare("Password", ErrorMessage = "Password doesn't match")]
        [Display(Name = "Confirm your password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Profile Picture")]
        public IFormFile ProfilePicture { get; set; }
        public string ProfilePictureUrl { get; set; }
    }
}
