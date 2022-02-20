namespace SummerTrainingSystem.Models
{
    public class StudentVM
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Level { get; set; }

        public double Gpa { get; set; }

        public int UniversityID { get; set; }

        public string Email { get; set; }

        public DepartmentVM Department { get; set; }
    }
}
