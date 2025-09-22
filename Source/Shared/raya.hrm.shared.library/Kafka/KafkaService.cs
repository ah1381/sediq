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
    public interface IKafkaErrorService
    {
        public Task<CustomActionResult> Produce(KafkaProducerModel model);
    }

    public class KafkaService : IKafkaErrorService
    {

        private readonly IProducer<string, string> _producer;

        private readonly KafkaConfig _config;

        public KafkaService(
         IOptions<KafkaConfig> kafkaConfig)
        {
            _config = kafkaConfig.Value;
            //string saslMechanismString = _config.SaslMechanism;
            ////var saslMechanism = (SaslMechanism)Enum.Parse(typeof(SaslMechanism), saslMechanismString, true);
            //var pcfg = new ProducerConfig
            //{
            //    BootstrapServers = _config.BootstrapServers,
            //    SecurityProtocol = SecurityProtocol.SaslPlaintext, // یا SaslSsl بسته به سرورت
            //    //SaslMechanism = Enum.Parse<SaslMechanism>(_config.SaslMechanism, true),
            //    SaslUsername = _config.User,
            //    SaslPassword = _config.Password,
            //    RequestTimeoutMs = 10000,
            //    ClientId = $"hrm-{Environment.MachineName}",
            //    Debug = "broker,protocol"
            //};

            var pcfg = new ProducerConfig
            {
                BootstrapServers = _config.BootstrapServers,
                ClientId = $"hrm-{Environment.MachineName}",
                Debug = "broker,protocol"
            };

            _producer = new ProducerBuilder<string, string>(pcfg).Build();
        }

        public async Task<CustomActionResult> Produce(KafkaProducerModel model)
        {
            CustomActionResult res = new CustomActionResult
            {
                ResponseType = 0,
                IsSuccess = true
            };
            if (model == null)
            {
                res.IsSuccess = false;
                res.ResponseType = 400;
                return res;
            }
            if (string.IsNullOrEmpty(model.Message))
            {
                res.IsSuccess = false;
                res.ResponseType = 400;
                return res;
            }

            using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(5)); // Application-level timeout

            try
            {
                var headers = new Headers() { { "Correlation-Id", Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()) } };
                if (model.Headers != null && model.Headers.Count > 0)
                {
                    foreach (var item in model.Headers)
                    {

                        headers.Add(item.Key, Encoding.UTF8.GetBytes(item.Value));
                    }
                }

                await _producer.ProduceAsync(_config.RequestTopic, new Message<string, string>
                {
                    Key = model.Key,
                    Value = model.Message,
                    Headers = headers

                }, cancellationTokenSource.Token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "|| stack trace: " + ex.StackTrace);
                res.IsSuccess = false;
                res.ResponseType = -1;
                res.ResponseDesc = ex.Message + "|| stack trace: " + ex.StackTrace;
            }


            return res;
        }
    }

}
