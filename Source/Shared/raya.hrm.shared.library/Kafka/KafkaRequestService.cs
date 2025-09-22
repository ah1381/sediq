using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Raya.Hrm.Shared.Library.Models.Base;
using Raya.Hrm.Shared.Library.Models.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Hrm.Shared.Library.Kafka
{
    public interface IKafkaRequestService
    {
        public Task<CustomActionResult> Produce(KafkaProducerModel model);
    }

    public class KafkaRequestService : IKafkaRequestService
    {
        private readonly IProducer<string, string> _producer;
        private readonly KafkaConfig _cfg;

        public KafkaRequestService(IOptions<KafkaConfig> options)
        {
            _cfg = options.Value;

            //var pcfg = new ProducerConfig
            //{
            //    BootstrapServers = _cfg.BootstrapServers,
            //    SecurityProtocol = SecurityProtocol.SaslPlaintext, // یا SaslSsl
            //    SaslMechanism = Enum.Parse<SaslMechanism>(_cfg.SaslMechanism, true),
            //    SaslUsername = _cfg.User,
            //    SaslPassword = _cfg.Password,
            //    RequestTimeoutMs = 10000,
            //    ClientId = $"hrm-{Environment.MachineName}",
            //    Debug = "broker,protocol"
            //};
            var pcfg = new ProducerConfig
            {
                BootstrapServers = _cfg.BootstrapServers,
                ClientId = $"hrm-{Environment.MachineName}",
                Debug = "broker,protocol"
            };

            _producer = new ProducerBuilder<string, string>(pcfg)
                           .SetErrorHandler((_, e) => Console.WriteLine($"KafkaErr: {e}"))
                           .Build();
        }

        public async Task<CustomActionResult> Produce(KafkaProducerModel model)
        {
            var res = new CustomActionResult { IsSuccess = true };
            if (model == null || string.IsNullOrWhiteSpace(model.Message))
                return res with { IsSuccess = false, ResponseType = 400 };

            try
            {
                var hdr = new Headers { { "Correlation-Id", Guid.NewGuid().ToByteArray() } };
                model.Headers?.ToList().ForEach(h => hdr.Add(h.Key, Encoding.UTF8.GetBytes(h.Value)));

                using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(10)); // Application-level timeout

                var r = await _producer.ProduceAsync(_cfg.RequestTopic,
                    new Message<string, string> { Key = model.Key, Value = model.Message, Headers = hdr }, cancellationTokenSource.Token);

                Console.WriteLine($"✔️ delivered to {r.TopicPartitionOffset}");
            }
            catch (ProduceException<string, string> ex)
            {
                Console.WriteLine($"❌ produce failed: {ex.Error.Reason}");
                res = res with { IsSuccess = false, ResponseType = -1, ResponseDesc = ex.Error.Reason };
            }

            return res;
        }
    }
}
