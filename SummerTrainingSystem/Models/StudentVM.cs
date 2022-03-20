using System.Collections.Generic;

namespace SummerTrainingSystem.Models
{
    public class StudentVM : IdentityUserVM
    {
        public StudentVM()
        {
            Trainnings = new List<TrainingVM>();
        }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Level { get; set; }

        public double Gpa { get; set; }

        public int UniversityID { get; set; }

        public int DepartmentId { get; set; }
        public DepartmentVM Department { get; set; }

        public List<TrainingVM> Trainnings { get; set; }
    }
}
