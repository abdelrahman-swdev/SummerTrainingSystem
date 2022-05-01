using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SummerTrainingSystem.Models;
using SummerTrainingSystemCore.Entities;
using SummerTrainingSystemCore.Enums;
using SummerTrainingSystemEF.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SummerTrainingSystem.Controllers
{
    public class ChatController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ChatController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("chat")]
        public async Task<IActionResult> ChatWith(string with)
        {
            var withUser = await _userManager.FindByNameAsync(with);
            if(withUser == null) return NotFound();

            Student IfStudentUser;
            Supervisor IfSupervisorUser;
            string withProfileUrl = string.Empty;

            if (await _userManager.IsInRoleAsync(withUser, Roles.Student.ToString()))
            {
                IfStudentUser = (Student)withUser;
                withProfileUrl = IfStudentUser.ProfilePictureUrl;
            }

            if (await _userManager.IsInRoleAsync(withUser, Roles.Supervisor.ToString()))
            {
                IfSupervisorUser = (Supervisor)withUser;
                withProfileUrl = IfSupervisorUser.ProfilePictureUrl;
            }

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
            .Take(4)
            .OrderBy(m => m.When)
            .ToList();

            return View(new ChatMessagesVM 
            { 
                Messages = messages, 
                WithEmail = with,
                WithPictureUrl = withProfileUrl
            });
        }
    }
}
