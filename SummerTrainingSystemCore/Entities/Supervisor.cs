using System.ComponentModel.DataAnnotations;

namespace SummerTrainingSystemCore.Entities
{
    public class Supervisor : AppUser
    {
        [Required]
        public int UniversityID { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
