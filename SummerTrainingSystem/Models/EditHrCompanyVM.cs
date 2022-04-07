using System;
using System.ComponentModel.DataAnnotations;

namespace SummerTrainingSystem.Models
{
    public class EditHrCompanyVM
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Company Name is required")]
        [Display(Name = "Company Name")]
        [MaxLength(256)]
        public string Name { get; set; }
        [Required(ErrorMessage = "City is required")]
        [MaxLength(256)]
        public string City { get; set; }
        [Required(ErrorMessage = "Country is required")]
        [MaxLength(256)]
        public string Country { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        [MaxLength(256)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [Display(Name = "Phone Number")]
        [MaxLength(256)]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Industry is required")]
        [MaxLength(256)]
        public string Industry { get; set; }
        [Required(ErrorMessage = "Specialities is required")]
        [MaxLength(256)]
        public string Specialities { get; set; }
        [MaxLength(256)]
        public string CompanyWebsite { get; set; }
        [Required(ErrorMessage = "About Company is required")]
        public string AboutCompany { get; set; }

        [Required(ErrorMessage = "Founding Date is required"), Display(Name = "Founded At"), DataType(DataType.Date)]
        public DateTime FoundedAt { get; set; }
        [Required(ErrorMessage = "Company Size is required")]
        [Display(Name = "Company Size")]
        public int CompanySizeId { get; set; }
    }
}
