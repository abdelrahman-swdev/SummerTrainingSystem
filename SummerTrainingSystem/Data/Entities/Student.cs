using System.ComponentModel.DataAnnotations.Schema;

namespace SummerTrainingSystem.Data.Entities
{
    [Table("Studets")]
    public class Student : ApplicationUser
    {
        public int Gpa { get; set; }
        public int Level { get; set; }
        public int UniversityId { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
