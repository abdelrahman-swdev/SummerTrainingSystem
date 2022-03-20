using System.ComponentModel.DataAnnotations;

namespace SummerTrainingSystem.Models
{
    public class CreateDepartmentVM
    {
        [Required, MaxLength(256)]
        public string Name { get; set; }

        [Required, MaxLength(50)]
        public string Abbreviation { get; set; }
    }
}
