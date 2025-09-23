using Log.Domain.Entities;
using Log.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Raya.Hrm.Shared.Library.GeneralErrorService;
using Raya.Hrm.Shared.Library.Models.Auth;
using Raya.Hrm.Shared.Library.Models.Configs;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Mongo Repository
builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection("MongoDb"));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var config = sp.GetRequiredService<IOptions<MongoDbConfig>>().Value;
    return new MongoClient(config.ConnectionString);
});

builder.Services.AddScoped(sp =>
{
    var mongoConfig = sp.GetRequiredService<IOptions<MongoDbConfig>>().Value;
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(mongoConfig.ConnectionString);
});

// Kafka Config
builder.Services.Configure<KafkaConsumerSettings>(builder.Configuration.GetSection("Kafka"));


// MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Log.Service.Handler.Commands.CreateErrorLogCommand).Assembly));

// Kafka Consumer
builder.Services.AddHostedService<KafkaConsumerService>();
builder.Services.AddScoped<IErrorMongoService, ErrorMongoService>();
builder.Services.AddScoped<IErrorLogRepository, ErrorLogRepository>();


builder.Services.AddControllers();


var jwtConfig = builder.Configuration.GetSection("Jwt").Get<JwtConfig>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuers = jwtConfig.Issuer,
        ValidAudiences = jwtConfig.Audience,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key))
    };
});

// Add Swagger JWT support WITHOUT duplicating SwaggerDoc
builder.Services.AddSwaggerGen(c =>
{
    // Only add security definition & requirement, assume SwaggerDoc is already registered in AddWebApiDependencies
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT token like: Bearer {your_token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Example API V1");
    c.RoutePrefix = string.Empty;
    app.Logger.LogInformation("Swagger UI enabled at root URL");
});

// Enable Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowSpecificOrigins");

app.MapControllers();
app.Run();
