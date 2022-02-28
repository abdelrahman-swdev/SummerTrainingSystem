using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SummerTrainingSystem.Models
{
    public class ResetPasswordVM
    {

        [Required(ErrorMessage = "Current Password is required")]
        [DataType(DataType.Password, ErrorMessage = "Password must be strong")]
        [MinLength(8, ErrorMessage = "Password should at least have 8 characters")]
        [Display(Name ="Current Password")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New Password is required")]
        [DataType(DataType.Password, ErrorMessage = "Password must be strong")]
        [MinLength(8, ErrorMessage = "Password should at least have 8 characters")]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm New Password is required")]
        [DataType(DataType.Password, ErrorMessage = "Password doesn't match")]
        [Compare("NewPassword", ErrorMessage = "Password doesn't match")]
        [Display(Name = "Confirm New password")]
        public string ConfirmPassword { get; set; }
    }
}
