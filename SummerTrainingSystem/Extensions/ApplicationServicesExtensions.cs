using Microsoft.Extensions.DependencyInjection;
using SummerTrainingSystemCore.Interfaces;
using SummerTrainingSystemEF.Data.Repositories;
using SummerTrainingSystemEF.Services;

namespace SummerTrainingSystem.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // add Generic Repository service to container
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // add auto mapper
            services.AddAutoMapper(typeof(Startup));

            // add account service
            services.AddScoped<IAccountService, AccountService>();

            return services;
        }
    }
}
