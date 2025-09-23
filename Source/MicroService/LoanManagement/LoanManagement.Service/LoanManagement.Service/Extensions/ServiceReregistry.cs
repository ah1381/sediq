using LoanManagement.Service.Services.ErrorService;
using LoanManagement.Service.Services.RequestDataService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Raya.Hrm.Shared.Library.GenaralAuthService;
using Raya.Hrm.Shared.Library.GeneralErrorService;
using Raya.Hrm.Shared.Library.Kafka;
using Raya.Hrm.Shared.Library.Models.Base;
using Raya.Hrm.Shared.Library.Models.Configs;
using System.Reflection;

namespace LoanManagement.Service.Extensions
{
    public static class ServiceReregistry
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Bind Config
            services.Configure<ErrorConfig>(configuration.GetSection("Error"));
            services.Configure<RequestConfig>(configuration.GetSection("Request"));
            services.Configure<ServicesDbConfig>(configuration.GetSection("ServicesConnectionStrings"));
            services.Configure<KafkaConfig>(configuration.GetSection("Kafka"));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            var errorConfig = configuration.GetSection("Error").Get<ErrorConfig>();
            var requestConfig = configuration.GetSection("Request").Get<RequestConfig>();


            // Consumer (Background service)
            services.AddHostedService<KafkaConsumerService>();

            // Error services
            if (errorConfig is { LogInKafka: true })
                services.AddSingleton<IKafkaErrorService, KafkaService>();
            else
                services.AddSingleton<IKafkaErrorService, NullKafkaErrorService>();

            // 🔹 Fix: Change services that depend on scoped DbContext to Scoped
            services.AddScoped<IErrorService, ErrorService>();
            services.AddScoped<IErrorLogInDbService, ErrorLogInDbService>();
            services.AddSingleton<IErrorLogToFIle, ErrorLogToFIle>();

            // Request services
            if (requestConfig is { LogInKafka: true })
                services.AddSingleton<IKafkaRequestService, KafkaRequestService>();
            else
                services.AddSingleton<IKafkaRequestService, NullKafkaRequestService>();

            // 🔹 Fix: Change services that depend on scoped DbContext to Scoped
            services.AddScoped<IRequestDataService, RequestDataService>();
            services.AddScoped<IRequestInDbService, RequestInDbService>();
            services.AddSingleton<IRequestLogToFIle, RequestLogToFIle>();
            services.AddSingleton<IAuthService, AuthService>();

            return services;
        }

        // Null services for Kafka fallback
        public class NullKafkaErrorService : IKafkaErrorService
        {
            public Task LogErrorAsync(object error) => Task.CompletedTask;

            public Task<CustomActionResult> Produce(KafkaProducerModel model)
                => throw new NotImplementedException();
        }

        public class NullKafkaRequestService : IKafkaRequestService
        {
            public Task<CustomActionResult> Produce(KafkaProducerModel model)
                => throw new NotImplementedException();
        }
    }
}
