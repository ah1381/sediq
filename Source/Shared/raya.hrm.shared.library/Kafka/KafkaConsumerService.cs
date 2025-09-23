using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Raya.Hrm.Shared.Library.Models.Configs;
using System.Text;

namespace Raya.Hrm.Shared.Library.Kafka
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly ILogger<KafkaConsumerService> _logger;
        private readonly KafkaConfig _config;
        private readonly IConsumer<string, string> _consumer;

        public KafkaConsumerService(
            IOptions<KafkaConfig> kafkaConfig,
            ILogger<KafkaConsumerService> logger)
        {
            _config = kafkaConfig.Value;
            _logger = logger;

            var cfg = new ConsumerConfig
            {
                BootstrapServers = _config.BootstrapServers,
                GroupId = _config.GroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<string, string>(cfg).Build();
            _consumer.Subscribe(_config.RequestTopic);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var result = _consumer.Consume(stoppingToken);

                    if (result != null)
                    {
                        var message = result.Message.Value;
                        var key = result.Message.Key;
                        var headers = result.Message.Headers
                            .ToDictionary(h => h.Key, h => Encoding.UTF8.GetString(h.GetValueBytes()));

                        _logger.LogInformation($"Received: Key={key}, Message={message}");
                    }
                }
                catch (ConsumeException ex)
                {
                    _logger.LogError($"Kafka consume error: {ex.Error.Reason}");
                }
                await Task.Delay(100, stoppingToken); // جلوگیری از Loop تند
            }
        }

        public override void Dispose()
        {
            _consumer.Close();
            _consumer.Dispose();
            base.Dispose();
        }
    }
}
