using Confluent.Kafka;
using Log.Domain.Entities;
using Log.Service.Handler.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Raya.Hrm.Shared.Library.Models.Raya.Hrm.Shared.Library.Models;
using System.Text.Json;

public class KafkaConsumerService : BackgroundService
{
    private readonly KafkaConsumerSettings _settings;
    private readonly IServiceProvider _serviceProvider;

    public KafkaConsumerService(IOptions<KafkaConsumerSettings> options, IServiceProvider serviceProvider)
    {
        _settings = options.Value;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = _settings.BootstrapServers,
            GroupId = _settings.GroupId,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe(_settings.Topics);

        while (!stoppingToken.IsCancellationRequested)
        {
            var result = consumer.Consume(stoppingToken);

            if (result?.Message?.Value != null)
            {
                var errorLog = JsonSerializer.Deserialize<ErrorModelMongo>(result.Message.Value);

                if (errorLog != null)
                {
                    using var scope = _serviceProvider.CreateScope();
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                    await mediator.Send(new CreateErrorLogCommand(errorLog), stoppingToken);
                }
            }
        }
    }

}
