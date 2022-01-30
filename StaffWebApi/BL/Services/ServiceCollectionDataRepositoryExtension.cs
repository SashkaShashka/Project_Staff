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
            // services.AddTransient<BillStatusRepository>();
            // services.AddTransient<BillRepository>();
        }

        public static void AddStaffServices(this IServiceCollection services)
        {
            services.AddTransient<PositionsService>();
            services.AddTransient<StaffService>();
            // services.AddTransient<BillsService>();
            // services.AddScoped<UsersService>();
        }
    }
}
