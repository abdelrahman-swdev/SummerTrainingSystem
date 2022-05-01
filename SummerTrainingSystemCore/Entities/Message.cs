using System;

namespace SummerTrainingSystemCore.Entities
{
    public class Message
    {
        public int Id { get; set; }

        public DateTime When { get; set; }
        public string SenderEmail { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public string Content { get; set; }
    }
}
