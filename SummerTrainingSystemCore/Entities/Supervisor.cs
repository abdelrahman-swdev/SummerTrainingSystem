using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SummerTrainingSystemCore.Entities
{
    public class Supervisor : IdentityUser
    {
        [Required, MaxLength(256)]
        public string FirstName { get; set; }

        [Required, MaxLength(256)]
        public string LastName { get; set; }

        [Required]
        public int UniversityID { get; set; }

        public int? DepartmentId { get; set; }
        public Department Department { get; set; }

        public string ProfilePictureUrl { get; set; }
    }
}
