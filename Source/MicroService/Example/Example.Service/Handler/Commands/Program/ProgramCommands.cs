using Example.Domain.Entities;
using Example.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GenaralAuthService;
using Raya.Hrm.Shared.Library.GeneralRepository;
using Raya.Hrm.Shared.Library.Models.Base;
using Raya.Hrm.Shared.Library.Utilities.Securities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Domain.Entities;

namespace Example.Service.Handler.Commands.Program
{
    public class CreateProgramCommand : IRequest<CustomActionResult<int>>
    {
        public ProgramCreateDto RequestModel { get; set; }
    }

    public class CreateProgramCommandHandler : IRequestHandler<CreateProgramCommand, CustomActionResult<int>>
    {
        private readonly IGenericRepository<ProgramEntity> _repository;

        public CreateProgramCommandHandler(IGenericRepository<ProgramEntity> repository)
        {
            _repository = repository;
            
        }
        public async Task<CustomActionResult<int>> Handle(CreateProgramCommand request, CancellationToken cancellationToken)
        {
            var res = new CustomActionResult<int>
            {
                IsSuccess = false,
                ResponseType = -2,

            };

            var result = await _repository.AddAsync(request.RequestModel);
            res.IsSuccess = true;
            res.ResponseType = 0;
            res.Data = result;
            return res;
        }
    }
}
