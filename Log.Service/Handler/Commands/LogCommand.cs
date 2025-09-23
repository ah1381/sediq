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

namespace Log.Service.Handler.Commands
{
    #region CreateCommands
    public class CreateErrorLogCommand : IRequest<Unit>
    {
        public ErrorModelMongo Log { get; }

        public CreateErrorLogCommand(ErrorModelMongo log)
        {
            Log = log;
        }
    }

    public class CreateErrorLogHandler : IRequestHandler<CreateErrorLogCommand, Unit>
    {
        private readonly IErrorMongoService _repository;

        public CreateErrorLogHandler(IErrorMongoService repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(CreateErrorLogCommand request, CancellationToken cancellationToken)
        {
            await _repository.LogErrorAsync(request.Log);
            return Unit.Value;
        }
    }

    #endregion

}
