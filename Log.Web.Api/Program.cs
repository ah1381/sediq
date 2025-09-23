using Log.Domain.Entities;
using Log.Domain.Interfaces;
using Log.Service.Services;
using MediatR;
using Microsoft.Extensions.Options;
using Raya.Hrm.Shared.Library.Models.Configs;

var builder = WebApplication.CreateBuilder(args);

// Mongo Repository
builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection("Mongo"));

// Kafka Config
builder.Services.Configure<KafkaConsumerSettings>(builder.Configuration.GetSection("Kafka"));

// MediatR
builder.Services.AddMediatR(cfg =>cfg.RegisterServicesFromAssembly(typeof(Log.Service.Handler.Commands.CreateErrorLogCommand).Assembly));

// Kafka Consumer
builder.Services.AddHostedService<KafkaConsumerService>();

builder.Services.AddControllers();
var app = builder.Build();

app.MapControllers();
app.Run();
