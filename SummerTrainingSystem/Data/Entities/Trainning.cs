using System;

namespace SummerTrainingSystem.Data.Entities
{
    public class Trainning
    {   
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
