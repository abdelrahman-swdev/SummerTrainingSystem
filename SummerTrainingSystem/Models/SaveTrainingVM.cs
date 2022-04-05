using System;
using System.ComponentModel.DataAnnotations;

namespace SummerTrainingSystem.Models
{
    public class SaveTrainingVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required"), MaxLength(256)]
        public string Title { get; set; }

        [Required,Display(Name ="Open Positions")]
        public int OpenPositions { get; set; }

        public int ApplicantsCount { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Starting Date is required"), Display(Name ="Starts At"), DataType(DataType.Date)]
        public DateTime StartAt { get; set; }
        [DataType(dataType:DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Finish Date is required"), Display(Name = "Ends At"), DataType(DataType.Date)]
        public DateTime EndAt { get; set; }

        [Required(ErrorMessage = "Department is required")]
        [Display(Name ="Department")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Training Type is required")]
        [Display(Name = "Training Type")]
        public int TrainingTypeId { get; set; }
        public string CompanyId { get; set; }
    }
}
