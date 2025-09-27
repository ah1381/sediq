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

namespace Example.Service.Handler.Commands.Student
{
    #region Create
    public class CreateStudentCommand : IRequest<CustomActionResult<long>>
    {
        public StudentCreateDto RequestModel { get; set; }
    }

    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, CustomActionResult<long>>
    {
        private readonly IGenericRepository<StudentEntity> _repository;
        private readonly IMapper _Mapper;


        public CreateStudentCommandHandler(IGenericRepository<StudentEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _Mapper = mapper;


        }
        public async Task<CustomActionResult<long>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var res = new CustomActionResult<long>
            {
                IsSuccess = false,
                ResponseType = -2,

            };

            var result = await _repository.AddAsync(_Mapper.Map<StudentEntity>(request.RequestModel));
            res.IsSuccess = true;
            res.ResponseType = 0;
            res.Data = result;
            return res;
        }
    }
    #endregion

    #region Edit
    public class EditStudentCommand : IRequest<CustomActionResult<StudentResponseDto>>
    {
        public StudentEditDto RequestModel { get; set; }
    }

    public class EditStudentCommandHandler : IRequestHandler<EditStudentCommand, CustomActionResult<StudentResponseDto>>
    {
        private readonly IGenericRepository<StudentEntity> _repository;
        private readonly IMapper _Mapper;


        public EditStudentCommandHandler(IGenericRepository<StudentEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _Mapper = mapper;


        }
        public async Task<CustomActionResult<StudentResponseDto>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            var res = new CustomActionResult<StudentResponseDto>
            {
                IsSuccess = false,
                ResponseType = -2,
            };

            var entity = await _repository.GetByIdAsync(request.RequestModel.Id);
            if (entity == null)
            {
                res.ResponseType = -1; // Not found
                return res;
            }

            // Map fields from DTO to entity
            _Mapper.Map(request.RequestModel, entity); // Mapping existing entity
            await _repository.UpdateAsync(entity);

            res.IsSuccess = true;
            res.ResponseType = 0;
            res.Data = _Mapper.Map<StudentResponseDto>(entity);
            return res;
        }
    }
    #endregion

    #region Delete
    public class DeleteStudentCommand : IRequest<CustomActionResult<bool>>
    {
        public StudentDeleteDto RequestModel { get; set; }
    }

    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, CustomActionResult<bool>>
    {
        private readonly IGenericRepository<StudentEntity> _repository;
        private readonly IMapper _Mapper;


        public DeleteStudentCommandHandler(IGenericRepository<StudentEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _Mapper = mapper;
        }
        public async Task<CustomActionResult<bool>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
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
