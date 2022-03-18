using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SummerTrainingSystem.Models
{
    public class CompanySizeVM
    {
        public int Id { get; set; }

        [MaxLength(256)]
        public string SizeName { get; set; }
        [MaxLength(256)]
        public string SizeRange { get; set; }
    }
}
