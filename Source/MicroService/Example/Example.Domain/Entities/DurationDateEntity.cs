using Raya.Hrm.Shared.Library.ModelS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Domain.Entities
{
    public class DurationDateEntity: BaseEntity
    {
        public DateOnly StartDur { get; set; }
        public DateOnly EndDur { get; set; }
        public string Name { get; set; }
        public virtual List<ScoreFormEntity> ScoreForm { get; set; }
    }
}
