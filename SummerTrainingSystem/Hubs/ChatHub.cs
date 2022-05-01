using Microsoft.AspNetCore.SignalR;
using SummerTrainingSystemCore.Entities;
using SummerTrainingSystemEF.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SummerTrainingSystem.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;

        public ChatHub(ApplicationDbContext context)
        {
            _context = context;
        }
        public override Task OnConnectedAsync()
        {
            var currentGroup = _context.Groups.FirstOrDefault(c => c.Name == Context.User.Identity.Name);
            if (currentGroup == null)
            {
                Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
                var group = new Group
                {
                    Name = Context.User.Identity.Name
                };
                _context.Groups.Add(group);
                _context.SaveChanges();
            }
            else
            {
                Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
            }
            return base.OnConnectedAsync();
        }

        public Task SendMessageToGroup(string receiver, string message)
        {
            // send message to specific group
            var group = _context.Groups.FirstOrDefault(g => g.Name == receiver);
            if (group != null)
            {
                var messageToAdd = new Message
                {
                    Content = message,
                    GroupId = group.Id,
                    SenderEmail = Context.User.Identity.Name,
                    When = DateTime.Now
                };
                group.Messages.Add(messageToAdd);
                _context.SaveChanges();
            }
            Clients.Group(Context.User.Identity.Name).SendAsync("ReceivePrivateMessage", Context.User.Identity.Name, message, DateTime.Now.ToString());
            Clients.Group(receiver).SendAsync("ReceivePrivateMessage", Context.User.Identity.Name, message, DateTime.Now.ToString());

            return Task.CompletedTask;
        }
    }
}
