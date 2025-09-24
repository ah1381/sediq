using Log.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Raya.Hrm.Shared.Library.GeneralErrorService;
using Raya.Hrm.Shared.Library.Models.Auth;

namespace Log.Domain.Extensions
{

    public static class ServiceReregistry
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
 
            // Kafka Consumer
            services.AddHostedService<KafkaConsumerService>();
            services.AddScoped<IErrorMongoService, ErrorMongoService>();
            services.AddScoped<IErrorLogRepository, ErrorLogRepository>();

            return services;
        }
    }
}