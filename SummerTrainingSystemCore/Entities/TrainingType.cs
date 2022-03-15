using System.ComponentModel.DataAnnotations;

namespace SummerTrainingSystemCore.Entities
{
    public class TrainingType
    {
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string TypeName { get; set; }
    }
}
