using SummerTrainingSystemCore.Entities;
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

        [Required, Display(Name ="Starts At")]
        public DateTime StartAt { get; set; }
        public DateTime CreatedAt { get; set; }

        [Required, Display(Name = "Ends At")]
        public DateTime EndAt { get; set; }

        [Display(Name ="Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
