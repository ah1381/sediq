using Log.Domain.Entities;
using Log.Domain.Interfaces;

using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Raya.Hrm.Shared.Library.GeneralErrorService;
using Raya.Hrm.Shared.Library.Models.Configs;

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
var app = builder.Build();

app.MapControllers();
app.Run();
