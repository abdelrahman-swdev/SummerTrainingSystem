using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SummerTrainingSystem.Models
{
    public class CreateRoleVM
    {
        [Required]
        public string Name { get; set; }
    }
}
