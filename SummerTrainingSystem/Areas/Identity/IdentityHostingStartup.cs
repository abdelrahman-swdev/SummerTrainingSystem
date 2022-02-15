using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(SummerTrainingSystem.Areas.Identity.IdentityHostingStartup))]
namespace SummerTrainingSystem.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}