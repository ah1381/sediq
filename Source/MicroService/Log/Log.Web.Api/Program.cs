using Log.Domain.Extensions;
using Log.Web.Api.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Raya.Hrm.Shared.Library.Models.Auth;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddWebApiDependencies(builder.Configuration);


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Log API V1");
    c.RoutePrefix = string.Empty;
    app.Logger.LogInformation("Swagger UI enabled at root URL");
});


app.UseCors("AllowSpecificOrigins");

app.MapControllers();

app.Run();
