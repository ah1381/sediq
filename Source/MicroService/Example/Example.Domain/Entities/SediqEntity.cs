using Raya.Hrm.Shared.Library.ModelS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Domain.Entities
{
    public class SediqEntity : BaseEntity
    {
        public long SediqCode { get; set; }
        public string sediqName { get; set; }
        public DateOnly StartDate { get; set; }
        public string? Description { get; set; }

        public virtual List<StudentEntity> Students { get; set; }
    }
}
