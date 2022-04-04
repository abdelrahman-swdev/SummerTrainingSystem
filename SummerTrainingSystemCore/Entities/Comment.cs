using System;

namespace SummerTrainingSystemCore.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime CreateAt { get; set; }
        public string StudentId { get; set; }
        public Student Student { get; set; }
        public string HrCompanyId { get; set; }
        public HrCompany HrCompany { get; set; }
    }
}
