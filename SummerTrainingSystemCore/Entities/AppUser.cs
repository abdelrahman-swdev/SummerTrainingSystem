using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SummerTrainingSystemCore.Entities
{
    public class AppUser : IdentityUser
    {
        [Required, MaxLength(256)]
        public string FirstName { get; set; }

        [Required, MaxLength(256)]
        public string LastName { get; set; }
    }
}
