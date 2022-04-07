using AspNetCoreHero.ToastNotification;
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
            
            // add training Repository service to container
            services.AddScoped<ITrainingRepository, TrainingRepository>();

            // add auto mapper
            services.AddAutoMapper(typeof(Startup));

            // add toast notificatios
            services.AddNotyf(options =>
            {
                options.DurationInSeconds = 5;
                options.IsDismissable = true;
                options.Position = NotyfPosition.TopRight;
            });

            // add account service
            services.AddScoped<IAccountService, AccountService>();

            return services;
        }
    }
}
