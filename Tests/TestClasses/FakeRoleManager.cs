using Microsoft.AspNetCore.Identity;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace Tests.TestClasses
{
    public class FakeRoleManager : RoleManager<IdentityRole>
    {
        private bool identityResultStatus;
        public FakeRoleManager(bool irs = false) :
            base(new Mock<IRoleStore<IdentityRole>>().Object, null, null, null, null)
        { identityResultStatus = irs; }

        public override Task<IdentityResult> CreateAsync(IdentityRole role)
        {
            return identityResultStatus == true ? Task.FromResult(IdentityResult.Success) : 
                Task.FromResult(new IdentityResult());
        }

        public override Task<IdentityRole> FindByIdAsync(string roleId)
        {
            return Task.FromResult(new IdentityRole());
        }

        public override Task<IdentityResult> DeleteAsync(IdentityRole role)
        {
            return identityResultStatus == true ? 
                Task.FromResult(IdentityResult.Success) : Task.FromResult(new IdentityResult());
        }

        public override IQueryable<IdentityRole> Roles => MockRoles();

        private IQueryable<IdentityRole> MockRoles()
        {
            var mockList = new Mock<IQueryable<IdentityRole>>();
            return mockList.Object;
        }
    }
}
