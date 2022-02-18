using Microsoft.AspNetCore.Identity;
using SummerTrainingSystemCore.Entities;
using SummerTrainingSystemCore.Enums;
using SummerTrainingSystemCore.Interfaces;
using System.Threading.Tasks;

namespace SummerTrainingSystemEF.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IdentityResult> CreateStudentAccount(Student student, string password)
        {
            var result = await _userManager.CreateAsync(student, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(student, Roles.Student.ToString());
            }
            return result;
        }

        public async Task<IdentityResult> CreateSupervisorAccount(Supervisor supervisor, string password)
        {
            var result = await _userManager.CreateAsync(supervisor, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(supervisor, Roles.Supervisor.ToString());
            }
            return result;
        }
    }
}
