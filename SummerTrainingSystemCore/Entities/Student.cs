using System.ComponentModel.DataAnnotations;

namespace SummerTrainingSystemCore.Entities
{
    public class Student : AppUser
    {
        [Required]
        public int Level { get; set; }

        [Required]
        public double Gpa { get; set; }

        [Required]
        public int UniversityID { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
