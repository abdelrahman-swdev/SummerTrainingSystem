using Microsoft.AspNetCore.Mvc;
using SummerTrainingSystem.Models;
using SummerTrainingSystemCore.Entities;
using SummerTrainingSystemEF.Data;
using System.Linq;

namespace SummerTrainingSystem.Controllers
{
    public class ChatController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChatController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("chat")]
        public IActionResult ChatWith(string with)
        {
            var withGroup = _context.Groups.FirstOrDefault(g => g.Name == with);
            if (withGroup == null)
            {
                // ال انا عايز اكلمه معملش كونكت قبل كده ف مالوش جروب باسمه اعمله واحد
                var group = new Group
                {
                    Name = with
                };
                _context.Groups.Add(group);
                _context.SaveChanges();
                withGroup = _context.Groups.FirstOrDefault(g => g.Name == with);
            }
            var myGroup = _context.Groups.FirstOrDefault(g => g.Name == User.Identity.Name);
            if (myGroup == null)
            {
                //  انا معملتش كونكت قبل كده ف ماليش جروب باسمي اعملي واحد
                var group = new Group
                {
                    Name = User.Identity.Name
                };
                _context.Groups.Add(group);
                _context.SaveChanges();
                myGroup = _context.Groups.FirstOrDefault(g => g.Name == User.Identity.Name);
            }

            var messages = _context.Messages.Where
            (m =>
                (m.GroupId == withGroup.Id || m.GroupId == myGroup.Id) &&
                (m.SenderEmail == User.Identity.Name || m.SenderEmail == withGroup.Name)
            )
            .OrderByDescending(m => m.When)
            .Take(10)
            .OrderBy(m => m.When)
            .ToList();

            return View(new ChatMessagesVM { Messages = messages, WithEmail = with });
        }
    }
}
