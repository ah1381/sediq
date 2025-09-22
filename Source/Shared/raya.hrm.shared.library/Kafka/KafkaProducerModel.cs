using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Hrm.Shared.Library.Kafka
{
    public record KafkaProducerModel
    {
        public string Message { get; set; }
        public string? Key { get; set; }
        public List<Header>? Headers { get; set; }
    }
    public record Header
    {
        public string? Key { get; set; }
        public string? Value { get; set; }
    }
}
