using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);


builder.AddProject<Projects.Example_Web_Api>("example-web-api");
builder.AddProject<Projects.Sediq_Web_Api>("sediq-web-api");
//builder.AddProject<Projects.Log_Web_Api>("log-web-api");

builder.Build().Run();
