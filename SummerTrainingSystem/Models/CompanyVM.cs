using System;
using System.ComponentModel.DataAnnotations;

namespace SummerTrainingSystem.Models
{
    public class CompanyVM
    {
        [Required, MaxLength(256)]
        public string Name { get; set; }

        [Required, MaxLength(256)]
        public string City { get; set; }

        [Required, MaxLength(256)]
        public string Country { get; set; }

        [Required, MaxLength(256)]
        public string Industry { get; set; }

        [Required]
        public string Specialities { get; set; }

        public string CompanyWebsite { get; set; }

        [Required]
        public string AboutCompany { get; set; }

        [DataType(dataType:DataType.Date)]
        public DateTime FoundedAt { get; set; }

        public int CompanySizeId { get; set; }

        public CompanySizeVM CompanySize{ get; set; }
    }
}
