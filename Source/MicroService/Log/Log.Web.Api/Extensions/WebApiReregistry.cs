using Log.Domain.Entities;
using Log.Domain.Interfaces;
using Log.Service.Handler.Commands;
using Log.Service.Handler.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Raya.Hrm.Shared.Library.GeneralErrorService;
using Raya.Hrm.Shared.Library.Kafka;
using Raya.Hrm.Shared.Library.Models.Auth;
using Raya.Hrm.Shared.Library.Models.Configs;
using System.Text;

namespace Log.Web.Api.Extensions
{
    public static class WebApiReregistry
    {
        public static IServiceCollection AddWebApiDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Mongo Repository
            services.Configure<MongoDbConfig>(configuration.GetSection("MongoDb"));

            services.AddSingleton<IMongoClient>(sp =>
            {
                var config = sp.GetRequiredService<IOptions<MongoDbConfig>>().Value;
                return new MongoClient(config.ConnectionString);
            });

            services.AddScoped(sp =>
            {
                var mongoConfig = sp.GetRequiredService<IOptions<MongoDbConfig>>().Value;
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase(mongoConfig.DatabaseName);
            });

            // Kafka Config
            services.Configure<KafkaConsumerSettings>(configuration.GetSection("Kafka"));


            // MediatR
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreateErrorLogCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetErrorLogsQuery).Assembly);

            });

            // Controllers 
            services.AddControllers();

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new()
                {
                    Title = "Log API",
                    Version = "v1",
                    Description = "A Clean Architecture API with CQRS, EF Core, Dapper, MediatR, and MongoDB"
                });
            });

            // CORS
            var corsConfig = configuration.GetSection("Cors").Get<CorsConfig>();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", builder =>
                {
                    builder.WithOrigins(corsConfig.AllowedOrigins)
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            return services;
        }

        public class CorsConfig
        {
            public string[] AllowedOrigins { get; set; }
        }

    }
}
