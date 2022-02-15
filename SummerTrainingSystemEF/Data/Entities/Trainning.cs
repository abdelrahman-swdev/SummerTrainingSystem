using System;
using System.ComponentModel.DataAnnotations;

namespace SummerTrainingSystemEF.Data.Entities
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
    }
}
