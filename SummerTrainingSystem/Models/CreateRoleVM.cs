using System.ComponentModel.DataAnnotations;

namespace SummerTrainingSystem.Models
{
    public class CreateRoleVM
    {
        [Required(ErrorMessage = "Role name is required")]
        public string Name { get; set; }
    }
}
