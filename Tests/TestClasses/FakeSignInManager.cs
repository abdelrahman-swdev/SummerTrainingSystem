using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Threading.Tasks;

namespace Tests.TestClasses
{
    public class FakeSignInManager : SignInManager<IdentityUser> 
    {
        public FakeSignInManager(UserManager<IdentityUser> user) : base(
            user,
              new Mock<IHttpContextAccessor>().Object,
              new Mock<IUserClaimsPrincipalFactory<IdentityUser>>().Object,
              new Mock<IOptions<IdentityOptions>>().Object,
              new Mock<ILogger<SignInManager<IdentityUser>>>().Object,
              new Mock<IAuthenticationSchemeProvider>().Object,
              new Mock<IUserConfirmation<IdentityUser>>().Object)
        { }


        public override Task RefreshSignInAsync(IdentityUser user)
        {
            return Task.FromResult(new IdentityUser());
        }
    }
}
