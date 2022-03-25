namespace SummerTrainingSystem.Models
{
    public class SupervisorVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int UniversityID { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string ProfilePictureUrl { get; set; }
        public DepartmentVM Department { get; set; }
    }
}
