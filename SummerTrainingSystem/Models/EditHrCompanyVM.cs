using SummerTrainingSystemCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SummerTrainingSystem.Models
{
    public class EditHrCompanyVM
    {
        [Required, MaxLength(256)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Industry { get; set; }
        public string Specialities { get; set; }
        public string CompanyWebsite { get; set; }
        public string AboutCompany { get; set; }
        public DateTime FoundedAt { get; set; }
        public int CompanySizeId { get; set; }
        public CompanySize CompanySize { get; set; }
    }
}
