using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SummerTrainingSystem.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<IdentityUser> GetLoggedInUser(this UserManager<IdentityUser> userManager, ClaimsPrincipal user)
        {
            return await userManager.FindByIdAsync(user.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
