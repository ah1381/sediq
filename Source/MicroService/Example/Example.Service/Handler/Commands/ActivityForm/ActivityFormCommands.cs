using AutoMapper;
using Example.Domain.Entities;
using Example.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;
using Raya.Hrm.Shared.Library.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Service.Handler.Commands.ActivityForm
{
    #region Create
    public class CreateActivityFormCommand : IRequest<CustomActionResult<long>>
    {
        public ActivityFormCreateDto RequestModel { get; set; }
    }

    public class CreateActivityFormCommandHandler : IRequestHandler<CreateActivityFormCommand, CustomActionResult<long>>
    {
        private readonly IGenericRepository<ActivityFormEntity> _repository;
        private readonly IMapper _Mapper;


        public CreateActivityFormCommandHandler(IGenericRepository<ActivityFormEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _Mapper = mapper;


        }
        public async Task<CustomActionResult<long>> Handle(CreateActivityFormCommand request, CancellationToken cancellationToken)
        {
            var res = new CustomActionResult<long>
            {
                IsSuccess = false,
                ResponseType = -2,

            };

            var result = await _repository.AddAsync(_Mapper.Map<ActivityFormEntity>(request.RequestModel));
            res.IsSuccess = true;
            res.ResponseType = 0;
            res.Data = result;
            return res;
        }
    }
    #endregion

    #region Edit
    public class EditActivityFormCommand : IRequest<CustomActionResult<ActivityFormResponseDto>>
    {
        public ActivityFormEditDto RequestModel { get; set; }
    }

    public class EditActivityFormCommandHandler : IRequestHandler<EditActivityFormCommand, CustomActionResult<ActivityFormResponseDto>>
    {
        private readonly IGenericRepository<ActivityFormEntity> _repository;
        private readonly IMapper _Mapper;


        public EditActivityFormCommandHandler(IGenericRepository<ActivityFormEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _Mapper = mapper;


        }
        public async Task<CustomActionResult<ActivityFormResponseDto>> Handle(EditActivityFormCommand request, CancellationToken cancellationToken)
        {
            var res = new CustomActionResult<ActivityFormResponseDto>
            {
                IsSuccess = false,
                ResponseType = -2,

            };

            await _repository.UpdateAsync(_Mapper.Map<ActivityFormEntity>(request.RequestModel));
            var result = await _repository.GetByIdAsync(request.RequestModel.Id);
            res.IsSuccess = true;
            res.ResponseType = 0;
            res.Data = _Mapper.Map<ActivityFormResponseDto>(result);
            return res;
        }
    }
    #endregion

    #region Delete
    public class DeleteActivityFormCommand : IRequest<CustomActionResult<bool>>
    {
        public ActivityFormDeleteDto RequestModel { get; set; }
    }

    public class DeleteActivityFormCommandHandler : IRequestHandler<DeleteActivityFormCommand, CustomActionResult<bool>>
    {
        private readonly IGenericRepository<ActivityFormEntity> _repository;
        private readonly IMapper _Mapper;


        public DeleteActivityFormCommandHandler(IGenericRepository<ActivityFormEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _Mapper = mapper;
        }
        public async Task<CustomActionResult<bool>> Handle(DeleteActivityFormCommand request, CancellationToken cancellationToken)
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
