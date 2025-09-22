using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Hrm.Shared.Library.Models.Base
{
    public interface IDbTransactionAccessor
    {
        IDbTransaction? CurrentTransaction { get; set; }
        IDbConnection? CurrentConnection { get; set; }
    }
    public class DbTransactionAccessor : IDbTransactionAccessor
    {
        public IDbTransaction? CurrentTransaction { get; set; }
        public IDbConnection? CurrentConnection { get; set; }
    }
}
