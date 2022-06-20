using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using SummerTrainingSystemCore.Entities;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Tests.TestClasses
{
    public class FakeUserManager : UserManager<IdentityUser>
    {
        public FakeUserManager(IUserStore<IdentityUser> userStore) : base(userStore/*new Mock<IUserStore<ApplicationUser>>().Object*/,
              new Mock<IOptions<IdentityOptions>>().Object,
              new Mock<IPasswordHasher<IdentityUser>>().Object,
              new IUserValidator<IdentityUser>[0],
              new IPasswordValidator<IdentityUser>[0],
              new Mock<ILookupNormalizer>().Object,
              new Mock<IdentityErrorDescriber>().Object,
              new Mock<IServiceProvider>().Object,
              new Mock<ILogger<UserManager<IdentityUser>>>().Object)
        { }

        public override Task<IdentityUser> GetUserAsync(ClaimsPrincipal cp)
        {
            return Task.FromResult<IdentityUser>(new Student());
        }


        public override Task<IdentityUser> FindByIdAsync(string id)
        {
            return Task.FromResult<IdentityUser>(new Student());
        }

        public override Task<IdentityResult> UpdateAsync(IdentityUser user)
        {
            return Task.FromResult<IdentityResult>(IdentityResult.Success);
        }



    }


    public class FakeUserManager2 : FakeUserManager
    {
        public FakeUserManager2(IUserStore<IdentityUser> userStore) : base(userStore) { }


        public override Task<IdentityUser> GetUserAsync(ClaimsPrincipal cp)
        {
            return Task.FromResult<IdentityUser>(null);
        }

        public override Task<IdentityResult> UpdateAsync(IdentityUser user)
        {
            return Task.FromResult<IdentityResult>(new IdentityResult());
        }

    }


    public class FakeUserManager3 : FakeUserManager
    {
        public FakeUserManager3(IUserStore<IdentityUser> userStore) : base(userStore) { }

        public override Task<IdentityResult> UpdateAsync(IdentityUser user)
        {
            return Task.FromResult<IdentityResult>(IdentityResult.Success);
        }


        public override Task<IdentityUser> FindByIdAsync(string id)
        {
            return Task.FromResult<IdentityUser>(new Supervisor());
        }

        public override Task<IdentityResult> ChangePasswordAsync(IdentityUser user, string currentPassword, string newPassword)
        {
            return Task.FromResult(new IdentityResult());
        }
    }


    public class FakeUserManager4 : FakeUserManager
    {
        public FakeUserManager4(IUserStore<IdentityUser> userStore) : base(userStore) { }

        public override Task<IdentityResult> UpdateAsync(IdentityUser user)
        {
            return Task.FromResult<IdentityResult>(new IdentityResult());
        }


        public override Task<IdentityUser> FindByIdAsync(string id)
        {
            return Task.FromResult<IdentityUser>(new Supervisor());
        }
    }

    //For password resest
    public class FakeUserManager5 : FakeUserManager
    {
        public FakeUserManager5(IUserStore<IdentityUser> userStore) : base(userStore) { }

        public override Task<IdentityUser> GetUserAsync(ClaimsPrincipal cp)
        {
            return Task.FromResult(new IdentityUser());
        }

        public override Task<IdentityResult> ChangePasswordAsync(IdentityUser user, string currentPassword, string newPassword)
        {
            return Task.FromResult(IdentityResult.Success);
        }
    }


    //For hr company
    public class FakeUserManager6 : FakeUserManager
    {
        public FakeUserManager6(IUserStore<IdentityUser> userStore) : base(userStore) { }

        public override Task<IdentityUser> FindByIdAsync(string id)
        {
            return Task.FromResult<IdentityUser>(new HrCompany());
        }
    }

    //For hr company
    public class FakeUserManager7 : FakeUserManager6
    {
        public FakeUserManager7(IUserStore<IdentityUser> userStore) : base(userStore) { }

        public override Task<IdentityResult> UpdateAsync(IdentityUser user)
        {
            return Task.FromResult<IdentityResult>(new IdentityResult());
        }
    }


    public class FakeUserManager8 : FakeUserManager
    {
        public FakeUserManager8(IUserStore<IdentityUser> userStore) : base(userStore) { }

        public override Task<IdentityUser> GetUserAsync(ClaimsPrincipal cp)
        {
            return Task.FromResult<IdentityUser>(new Supervisor());
        }
    }
}
