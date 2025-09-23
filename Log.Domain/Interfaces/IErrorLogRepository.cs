using Log.Domain.Entities;
using Raya.Hrm.Shared.Library.Models.Raya.Hrm.Shared.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Log.Domain.Interfaces
{
    public interface IErrorLogRepository
    {
        Task InsertAsync(ErrorModelMongo log);
        Task<IEnumerable<ErrorModelMongo>> GetLogsAsync(string? serviceName, string? logType);
    }
}
