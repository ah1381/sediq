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
using AutoMapper;

namespace Example.Service.Handler.Commands.Program
{
    #region Create
    public class CreateProgramCommand : IRequest<CustomActionResult<long>>
    {
        public ProgramCreateDto RequestModel { get; set; }
    }

    public class CreateProgramCommandHandler : IRequestHandler<CreateProgramCommand, CustomActionResult<long>>
    {
        private readonly IGenericRepository<ProgramEntity> _repository;
        private readonly IMapper _Mapper;


        public CreateProgramCommandHandler(IGenericRepository<ProgramEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _Mapper = mapper;


        }
        public async Task<CustomActionResult<long>> Handle(CreateProgramCommand request, CancellationToken cancellationToken)
        {
            var res = new CustomActionResult<long>
            {
                IsSuccess = false,
                ResponseType = -2,

            };

            var result = await _repository.AddAsync(_Mapper.Map<ProgramEntity>(request.RequestModel));
            res.IsSuccess = true;
            res.ResponseType = 0;
            res.Data = result;
            return res;
        }
    }
    #endregion

    #region Edit
    public class EditProgramCommand : IRequest<CustomActionResult<ProgramResponseDto>>
    {
        public ProgramEditDto RequestModel { get; set; }
    }

    public class EditProgramCommandHandler : IRequestHandler<EditProgramCommand, CustomActionResult<ProgramResponseDto>>
    {
        private readonly IGenericRepository<ProgramEntity> _repository;
        private readonly IMapper _Mapper;


        public EditProgramCommandHandler(IGenericRepository<ProgramEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _Mapper = mapper;


        }
        public async Task<CustomActionResult<ProgramResponseDto>> Handle(EditProgramCommand request, CancellationToken cancellationToken)
        {
            var res = new CustomActionResult<ProgramResponseDto>
            {
                IsSuccess = false,
                ResponseType = -2,

            };

            await _repository.UpdateAsync(_Mapper.Map<ProgramEntity>(request.RequestModel));
            var result = await _repository.GetByIdAsync(request.RequestModel.Id);
            res.IsSuccess = true;
            res.ResponseType = 0;
            res.Data = _Mapper.Map<ProgramResponseDto>(result);
            return res;
        }
    }
    #endregion

    #region Delete
    public class DeleteProgramCommand : IRequest<CustomActionResult<bool>>
    {
        public ProgramDeleteDto RequestModel { get; set; }
    }

    public class DeleteProgramCommandHandler : IRequestHandler<DeleteProgramCommand, CustomActionResult<bool>>
    {
        private readonly IGenericRepository<ProgramEntity> _repository;
        private readonly IMapper _Mapper;


        public DeleteProgramCommandHandler(IGenericRepository<ProgramEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _Mapper = mapper;
        }
        public async Task<CustomActionResult<bool>> Handle(DeleteProgramCommand request, CancellationToken cancellationToken)
        {
            var res = new CustomActionResult<bool>
            {
                IsSuccess = false,
                ResponseType = -2,
            };

            await _repository.DeleteAsync(request.RequestModel.Id);
            res.IsSuccess = true;
            res.ResponseType = 0;
            res.Data = true;
            return res;
        }
    }
    #endregion
}
