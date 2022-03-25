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

        public byte Level { get; set; }

        public float Gpa { get; set; }

        public int UniversityID { get; set; }

        public DepartmentVM Department { get; set; }

        public List<TrainingVM> Trainnings { get; set; }
        public string ProfilePictureUrl { get; set; }

    }
}
