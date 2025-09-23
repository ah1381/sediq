using Log.Domain.Entities;
using Log.Domain.Interfaces;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralErrorService;
using Raya.Hrm.Shared.Library.Models.Raya.Hrm.Shared.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.Service.Handler.Queries
{
    public class GetErrorLogsQuery : IRequest<IEnumerable<ErrorModelMongo>>
    {
        public string? ServiceName { get; }
        public string? LogType { get; }

        public GetErrorLogsQuery(string? serviceName, string? logType)
        {
            ServiceName = serviceName;
            LogType = logType;
        }
    }

    public class GetErrorLogsHandler : IRequestHandler<GetErrorLogsQuery, IEnumerable<ErrorModelMongo>>
    {
        private readonly IErrorLogRepository _repository;

        public GetErrorLogsHandler(IErrorLogRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ErrorModelMongo>> Handle(GetErrorLogsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetLogsAsync(request.ServiceName,request.LogType);
        }
    }
}
