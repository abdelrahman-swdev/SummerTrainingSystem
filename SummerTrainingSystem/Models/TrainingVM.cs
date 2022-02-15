using SummerTrainingSystemEF.Data.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace SummerTrainingSystem.Models
{
    public class TrainingVM
    {
        public int Id { get; set; }

        [Required, MaxLength(256)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartAt { get; set; }
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime EndAt { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
