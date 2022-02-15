using System.ComponentModel.DataAnnotations;

namespace SummerTrainingSystem.Data.Entities
{
    public class Department
    {
        public int Id { get; set; }

        [Required, MaxLength(256)]
        public string Name { get; set; }

        [Required, MaxLength(50)]
        public string Abbreviation { get; set; }
    }
}
