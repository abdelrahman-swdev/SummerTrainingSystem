using Microsoft.AspNetCore.Identity;
using SummerTrainingSystemCore.Entities;
using System.Threading.Tasks;

namespace SummerTrainingSystemCore.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> CreateStudentAccount(Student student, string password);
        Task<IdentityResult> CreateSupervisorAccount(Supervisor supervisor, string password);
    }
}
