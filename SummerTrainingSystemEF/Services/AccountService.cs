using Microsoft.AspNetCore.Identity;
using SummerTrainingSystemCore.Entities;
using SummerTrainingSystemCore.Interfaces;
using System.Threading.Tasks;

namespace SummerTrainingSystemEF.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AccountService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IdentityResult> CreateStudentAccount(Student student, string password)
        {
            return await _userManager.CreateAsync(student, password);
        }

        public async Task<IdentityResult> CreateSupervisorAccount(Supervisor supervisor, string password)
        {
            return await _userManager.CreateAsync(supervisor, password);
        }
    }
}
