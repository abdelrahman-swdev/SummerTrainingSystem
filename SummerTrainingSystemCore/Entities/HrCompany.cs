using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace SummerTrainingSystemCore.Entities
{
    public class HrCompany : IdentityUser
    { 
    
        [Required, MaxLength(256)]
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Industry { get; set; }
        public string Specialities { get; set; }
        public string CompanyWebsite { get; set; }
        public string AboutCompany { get; set; }
        public DateTime FoundedAt { get; set; }
        public int CompanySizeId { get; set; }
        public CompanySize CompanySize { get; set; }
    }
}
