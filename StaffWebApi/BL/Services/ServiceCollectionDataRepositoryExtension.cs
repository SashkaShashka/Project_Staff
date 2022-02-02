using StaffDBContext_Code_first.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace StaffWebApi.BL.Services
{
    public static class ServiceCollectionDataRepositoryExtension
    {
        public static void AddStaffRepositories(this IServiceCollection services)
        {
            services.AddTransient<PositionRepository>();
            services.AddTransient<StaffRepository>();
        }

        public static void AddStaffServices(this IServiceCollection services)
        {
            services.AddTransient<PositionsService>();
            services.AddTransient<StaffService>();
            services.AddScoped<UsersService>();
        }
    }
}
