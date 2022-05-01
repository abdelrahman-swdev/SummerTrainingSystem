using SummerTrainingSystemCore.Entities;
using System.Collections.Generic;

namespace SummerTrainingSystem.Models
{
    public class ChatMessagesVM
    {
        public ChatMessagesVM()
        {
            Messages = new List<Message>();
        }
        public string WithEmail { get; set; }
        public List<Message> Messages { get; set; }
    }
}
