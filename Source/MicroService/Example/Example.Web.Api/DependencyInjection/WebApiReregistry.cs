
using Example.Service.Handler.Commands.Program;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Raya.Hrm.Shared.Library.GeneralErrorService;
using Raya.Hrm.Shared.Library.GeneralRequestService;
using Raya.Hrm.Shared.Library.Models.Auth;
using Raya.Hrm.Shared.Library.Models.Configs;


namespace Example.Web.Api.DependencyInjection
{
    public static class WebApiReregistry
    {
        public static IServiceCollection AddWebApiDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // JWT
            services.Configure<JwtConfig>(configuration.GetSection("Jwt"));

            // MongoDB
            //services.Configure<MongoDbConfig>(configuration.GetSection("MongoDb"));

            //services.AddSingleton<IMongoClient>(sp =>
            //{
            //    var mongoConfig = sp.GetRequiredService<IOptions<MongoDbConfig>>().Value;
            //    return new MongoClient(mongoConfig.ConnectionString);
            //});

            //services.AddScoped(sp =>
            //{
            //    var mongoConfig = sp.GetRequiredService<IOptions<MongoDbConfig>>().Value;
            //    var client = sp.GetRequiredService<IMongoClient>();
            //    return client.GetDatabase(mongoConfig.ConnectionString);
            //});

            // Register Mongo logging services
            //services.AddSingleton<IErrorMongoService, ErrorMongoService>();
            //services.AddSingleton<IRequestMongoService, RequestMongoService>();

            // MediatR
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreateProgramCommand).Assembly);
            });

            // Controllers + FluentValidation
            services.AddControllers()
                    .AddFluentValidation(fv =>
                    {
                        fv.RegisterValidatorsFromAssemblyContaining<Program>();
                    });

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new()
                {
                    Title = "Example API",
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
