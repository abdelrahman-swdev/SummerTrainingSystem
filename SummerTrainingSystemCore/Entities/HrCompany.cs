using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SummerTrainingSystemCore.Entities
{
    public class HrCompany : IdentityUser
    {
        public HrCompany()
        {
            Comments = new List<Comment>();
        }
    
        [Required, MaxLength(255)]
        public string Name { get; set; }

        [Required, MaxLength(255)]
        public string City { get; set; }

        [Required, MaxLength(255)]
        public string Country { get; set; }

        [Required, MaxLength(255)]
        public string Industry { get; set; }

        [Required, MaxLength(255)]
        public string Specialities { get; set; }

        [MaxLength(255)]
        public string CompanyWebsite { get; set; }
        [Required]
        public string AboutCompany { get; set; }
        public DateTime FoundedAt { get; set; }
        public int CompanySizeId { get; set; }
        public CompanySize CompanySize { get; set; }
        public string ProfilePictureUrl { get; set; }
        public List<Comment> Comments { get; set; }

    }
}
