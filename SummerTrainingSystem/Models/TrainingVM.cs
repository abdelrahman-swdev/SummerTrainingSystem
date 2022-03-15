using System;

namespace SummerTrainingSystem.Models
{
    public class TrainingVM
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime EndAt { get; set; }

        public int DepartmentId { get; set; }
        public DepartmentVM Department { get; set; }

        public int TrainingTypeId { get; set; }
        public TrainingTypeVM TrainingType { get; set; }
        public string CompanyId { get; set; }
        public CompanyVM Company { get; set; }
    }
}
