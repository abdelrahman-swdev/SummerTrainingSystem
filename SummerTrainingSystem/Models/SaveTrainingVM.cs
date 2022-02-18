using System;
using System.ComponentModel.DataAnnotations;

namespace SummerTrainingSystem.Models
{
    public class SaveTrainingVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required"), MaxLength(256)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Starting Date is required"), Display(Name ="Starts At")]
        public DateTime StartAt { get; set; }
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "Finish Date is required"), Display(Name = "Ends At")]
        public DateTime EndAt { get; set; }

        [Required(ErrorMessage = "Department is required")]
        [Display(Name ="Department")]
        public int DepartmentId { get; set; }
    }
}
