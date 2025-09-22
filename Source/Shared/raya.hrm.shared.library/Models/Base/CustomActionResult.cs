using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Hrm.Shared.Library.Models.Base
{
    public record CustomActionResult
    {
        public bool IsSuccess { get; set; }
        public string ResponseDesc { get; set; }
        public decimal ResponseType { get; set; }
        public int TotalCount { get; set; }

    }
    public record CustomActionResult<T> : CustomActionResult
    {
        public T Data { get; set; }
    }
}
