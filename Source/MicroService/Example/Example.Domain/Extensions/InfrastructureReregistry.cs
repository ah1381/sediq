using Example.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Raya.Hrm.Shared.Library.GeneralRepository;

namespace Example.Domain.Extensions
{
    public static class InfrastructureReregistry
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            var connectionStringLog = configuration.GetConnectionString("LoggingDatabase");

            services.AddDbContext<LoggingDbContext>(options =>
            {
                options.UseNpgsql(connectionStringLog);
            });


            services.AddScoped<DbContext, AppDbContext>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
