using SummerTrainingSystemCore.Entities;
using System;
using System.Collections.Generic;

namespace SummerTrainingSystem.Models
{
    public class CommentVM
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime CreateAt { get; set; }
        public string HrCompanyId { get; set; }
        public string StudentId { get; set; }
        public Student Student { get; set; }
    }
}
