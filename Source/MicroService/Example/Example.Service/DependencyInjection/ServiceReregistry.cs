
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Raya.Hrm.Shared.Library.GenaralAuthService;
using Raya.Hrm.Shared.Library.GeneralErrorService;
using Raya.Hrm.Shared.Library.Kafka;
using Raya.Hrm.Shared.Library.Models.Base;
using Raya.Hrm.Shared.Library.Models.Configs;
using System.Reflection;

namespace Example.Service.DependencyInjection
{
    public static class ServiceReregistry
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Bind Config
           
            services.Configure<ServicesDbConfig>(configuration.GetSection("ConnectionStrings"));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }

    }
}
