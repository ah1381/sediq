using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Hrm.Shared.Library.Models.Configs
{
    public record QueryConfig
    {
        public int MaxRowReturn { get; set; }
        public int MaxPageSize { get; set; }

    }
}
