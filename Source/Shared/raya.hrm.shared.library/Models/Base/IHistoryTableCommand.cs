using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Hrm.Shared.Library.Models.Base
{
    public interface IHistoryTableCommand
    {
        string TableName { get; }
        int AffectedRowId { get; }
    }
}
