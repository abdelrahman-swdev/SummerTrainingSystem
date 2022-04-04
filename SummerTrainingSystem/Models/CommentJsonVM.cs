using System.ComponentModel.DataAnnotations;

namespace SummerTrainingSystem.Models
{
    public class CommentJsonVM
    {
        public string HrCompanyId { get; set; }
        [Required]
        public string Comment { get; set; }
    }
}
