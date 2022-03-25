using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SummerTrainingSystem.Models
{
    public class EditSupervisorProfileVM
    {
        public string Id { get; set; }

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
        public string Email { get; set; }
        public int UniversityID { get; set; }
        public int DepartmentId { get; set; }
    }
}
