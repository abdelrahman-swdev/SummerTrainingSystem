using System.ComponentModel.DataAnnotations;

namespace SummerTrainingSystem.Models
{
    public class TrainingTypeVM
    {
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string TypeName { get; set; }
    }
}
