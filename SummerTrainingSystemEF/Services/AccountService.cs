using Microsoft.AspNetCore.Identity;
using SummerTrainingSystemCore.Entities;
using SummerTrainingSystemCore.Enums;
using SummerTrainingSystemCore.Interfaces;
using SummerTrainingSystemEF.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SummerTrainingSystemEF.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountService(
            UserManager<IdentityUser> userManager,
            ApplicationDbContext context,
            SignInManager<IdentityUser> signInManager
            )
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }
        public async Task<IdentityResult> CreateStudentAccountAsync(Student student, string password)
        {
            var result = await _userManager.CreateAsync(student, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(student, Roles.Student.ToString());
            }
            return result;
        }

        public async Task<IdentityResult> CreateSupervisorAccountAsync(Supervisor supervisor, string password)
        {
            var result = await _userManager.CreateAsync(supervisor, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(supervisor, Roles.Supervisor.ToString());
            }
            return result;
        }

        public async Task<IdentityResult> CreateCompanyAccountAsync(HrCompany company, string password)
        {
            var result = await _userManager.CreateAsync(company, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(company, Roles.Company.ToString());
            }
            return result;
        }

        public async Task<SignInResult> LoginAsync(int universityId, string password)
        {
            var student = _context.Students.FirstOrDefault(c => c.UniversityID == universityId);
            if (student != null)
            {
                return await _signInManager.PasswordSignInAsync(student, password, false, false);
            }
            var supervisor = _context.Supervisors.FirstOrDefault(c => c.UniversityID == universityId);
            if (supervisor != null)
            {
                return await _signInManager.PasswordSignInAsync(supervisor, password, false, false);
            }

            return SignInResult.Failed;
        }

        public bool CheckIsUniversityIdExists(int universityId)
        {
            var student = _context.Students.FirstOrDefault(c => c.UniversityID == universityId);
            if (student != null) return true;
            var supervisor = _context.Supervisors.FirstOrDefault(c => c.UniversityID == universityId);
            if (supervisor != null) return true;

            return false;
        }

        public bool CheckIsEmailExists(string email)
        {
            var user = _userManager.Users.SingleOrDefault(s => s.Email == email);
            if(user != null) return true;
            return false;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<SignInResult> LoginAsCompanyAsync(string email, string password)
        {
            var company = await _userManager.FindByEmailAsync(email);
            if (company != null)
            {
                return await _signInManager.PasswordSignInAsync(company, password, false, false);
            }

            return SignInResult.Failed;
        }
    }
}
