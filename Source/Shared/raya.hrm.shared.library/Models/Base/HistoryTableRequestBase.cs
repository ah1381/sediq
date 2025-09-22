using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Raya.Hrm.Shared.Library.Models.Base
{
    public class HistoryTableRequestBase<T>
    {
        public string TableName { get; set; } = default!;
        public long AffectedRowId { get; set; }
        public string? ChangedByUserId { get; set; }
        public T? PreviousData { get; set; }

        public string? ToJson()
        {
            return PreviousData != null ? JsonSerializer.Serialize(PreviousData) : null;
        }
    }
}
