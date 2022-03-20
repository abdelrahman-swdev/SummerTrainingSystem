using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SummerTrainingSystemCore.Entities
{
    public class Student : IdentityUser
    {
        public Student()
        {
            Trainnings = new List<Trainning>();
        }

        public List<Trainning> Trainnings { get; set; }

        [Required, MaxLength(256)]
        public string FirstName { get; set; }

        [Required, MaxLength(256)]
        public string LastName { get; set; }

        [Required]
        public int Level { get; set; }

        [Required]
        public double Gpa { get; set; }

        [Required]
        public int UniversityID { get; set; }

        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
