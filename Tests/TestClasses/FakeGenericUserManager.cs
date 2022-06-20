using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Tests.TestClasses
{
    public class FakeGenericUserManager : UserManager<IdentityUser>
    {
        IdentityUser fakeInstance;
        private bool identityStatus;
        private string currentRole;
        public FakeGenericUserManager(string cr = "", bool status = false, IdentityUser instance = null)
            : base(new Mock<IUserStore<IdentityUser>>().Object,
              new Mock<IOptions<IdentityOptions>>().Object,
              new Mock<IPasswordHasher<IdentityUser>>().Object,
              new IUserValidator<IdentityUser>[0],
              new IPasswordValidator<IdentityUser>[0],
              new Mock<ILookupNormalizer>().Object,
              new Mock<IdentityErrorDescriber>().Object,
              new Mock<IServiceProvider>().Object,
              new Mock<ILogger<UserManager<IdentityUser>>>().Object)
        { fakeInstance = instance; identityStatus = status; currentRole = cr; }

        public override Task<IdentityUser> GetUserAsync(ClaimsPrincipal principal)
        {
            return Task.FromResult(fakeInstance);
        }

        public override Task<bool> IsInRoleAsync(IdentityUser user, string role)
        {
            if (currentRole == role)
            {
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        public override Task<IdentityUser> FindByIdAsync(string userId)
        {
            if (fakeInstance != null) { return Task.FromResult(fakeInstance); }

            return Task.FromResult<IdentityUser>(null);
        }

        public override Task<IdentityResult> DeleteAsync(IdentityUser user)
        {
            if (identityStatus) { return Task.FromResult(IdentityResult.Success); }

            return Task.FromResult(new IdentityResult());
        }


        public override Task<IdentityResult> UpdateAsync(IdentityUser user)
        {
            if (fakeInstance != null) { return Task.FromResult(IdentityResult.Success); }

            return Task.FromResult<IdentityResult>(null);
        }

        public override Task<IdentityUser> FindByNameAsync(string userName)
        {
            if (fakeInstance != null) { return Task.FromResult(fakeInstance); }

            return Task.FromResult<IdentityUser>(null);
        }
    }
}
