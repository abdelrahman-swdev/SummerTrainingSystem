using System;
using System.ComponentModel.DataAnnotations;

namespace SummerTrainingSystemCore.Entities
{
    public class Trainning
    {   
        public int Id { get; set; }

        [Required, MaxLength(256)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int TrainingTypeId { get; set; }
        public TrainingType TrainingType { get; set; }
        public string CompanyId { get; set; }
        public HrCompany Company { get; set; }
    }
}
