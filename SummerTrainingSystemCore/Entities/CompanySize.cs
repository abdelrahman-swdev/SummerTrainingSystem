using System.ComponentModel.DataAnnotations;

namespace SummerTrainingSystemCore.Entities
{
    public class CompanySize
    {
        public int Id { get; set; }

        [MaxLength(256)]
        public string SizeName { get; set; }

        [MaxLength(256)]
        public string SizeRange { get; set; }
    }
}