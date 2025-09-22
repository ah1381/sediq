using Raya.Hrm.Shared.Library.Utilities.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Hrm.Shared.Library.Models.Base
{
    public record BaseQueryRequest
    {
        public int RowCountToReturn { get; set; }
    }

    //public record BaseFilterQueryRequest
    //{
    //    public Filter<int> RowCountToReturn { get; set; }
    //}
}
