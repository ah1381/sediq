using LoanManagement.Domain.Extensions;
using LoanManagement.Service.Extensions;
using LoanManagement.WebApi.Extensions;
using LoanManagement.WebApi.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Raya.Hrm.Shared.Library.Models.Auth;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddWebApiDependencies(builder.Configuration);

// Add JWT Authentication
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

// Build app
var app = builder.Build();

// Middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

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
