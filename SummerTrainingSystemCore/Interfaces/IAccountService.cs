﻿using Microsoft.AspNetCore.Identity;
using SummerTrainingSystemCore.Entities;
using System.Threading.Tasks;

namespace SummerTrainingSystemCore.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> CreateStudentAccountAsync(Student student, string password);
        Task<IdentityResult> CreateSupervisorAccountAsync(Supervisor supervisor, string password);

        bool CheckIsUniversityIdExists(int universityId);

        Task<SignInResult> LoginAsync(int universityId, string password);
        Task LogoutAsync();
    }
}
