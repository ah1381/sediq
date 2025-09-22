using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Hrm.Shared.Library.Models.Auth
{
    public record JwtConfig
    {
        public string[] Issuer { get; set; }
        public string[] Audience { get; set; }
        public string Key { get; set; }
    }
}
