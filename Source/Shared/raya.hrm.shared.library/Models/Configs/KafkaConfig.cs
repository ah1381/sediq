using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Hrm.Shared.Library.Models.Configs
{
    public class KafkaConfig
    {
        public string BootstrapServers { get; set; }
        public string ErrorTopic { get; set; }
        public string RequestTopic { get; set; }
        public string SaslMechanism { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
