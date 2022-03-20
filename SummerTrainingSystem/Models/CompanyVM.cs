﻿using System;
using System.ComponentModel.DataAnnotations;

namespace SummerTrainingSystem.Models
{
    public class CompanyVM
    {
        public string Name { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
        public string Email { get; set; }

        public string Industry { get; set; }
        public string PhoneNumber { get; set; }

        public string Specialities { get; set; }

        public string CompanyWebsite { get; set; }

        public string AboutCompany { get; set; }

        public DateTime FoundedAt { get; set; }

        public int CompanySizeId { get; set; }

        public CompanySizeVM CompanySize{ get; set; }
    }
}
