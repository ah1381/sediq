using AutoMapper;
using Example.Domain.Entities;
using Example.Service.Models.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Raya.Hrm.Shared.Library.GeneralRepository;
using Raya.Hrm.Shared.Library.Models.Base;
using Raya.Hrm.Shared.Library.Utilities;

namespace Example.Service.Handler.Commands.Student
{
    #region create
    public class CreateStudentCommand : IRequest<CustomActionResult<StudentResponseDto>>
    {
        public StudentCreateModel RequestModel { get; set; }
    }

    public class CreateStudentHandler : IRequestHandler<CreateStudentCommand, CustomActionResult<StudentResponseDto>>
    {
        private readonly IGenericRepository<StudentEntity> _repository;
        private readonly IGenericRepository<SediqEntity> _sediqRepository;
        private readonly IMapper _mapper;

        public CreateStudentHandler(IGenericRepository<StudentEntity> repository, IMapper mapper, IGenericRepository<SediqEntity> sediqRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _sediqRepository = sediqRepository;
        }

        public async Task<CustomActionResult<StudentResponseDto>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<StudentEntity>(request.RequestModel);
            
            // رمزنگاری پسورد
            if (!string.IsNullOrEmpty(entity.Password))
            {
                entity.Password = BCrypt.Net.BCrypt.HashPassword(entity.Password);
            }
            
            var sediq = await _sediqRepository.GetByIdAsync(entity.sediqRowId.Value);
            var sediqs = await _sediqRepository.GetQueryable().Include(x => x.Students).Where(x => x.RowId == entity.sediqRowId.Value).ToListAsync();
            entity.StudentCode = sediq.SediqCode.ToString() + entity.MembershipDate.Value.Year.ToString() + sediq.Students.Count.ToString();
            var saveEntity = await _repository.AddAsync(entity);
            var result = _mapper.Map<StudentResponseDto>(saveEntity);
            return Result.Created(result, "Student created successfully");
        }
    }
    #endregion

    #region update
    public class UpdateStudentCommand : IRequest<CustomActionResult<StudentResponseDto>>
    {
        public StudentUpdateModel Student { get; set; }

        public UpdateStudentCommand()
        {
        }

        public UpdateStudentCommand(StudentUpdateModel student)
        {
            Student = student;
        }
    }

    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, CustomActionResult<StudentResponseDto>>
    {
        private readonly IGenericRepository<StudentEntity> _repository;
        private readonly IMapper _mapper;

        public UpdateStudentCommandHandler(IGenericRepository<StudentEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomActionResult<StudentResponseDto>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Student.RowId.Value);
            if (entity.IsNull())
            {
                return Result.NotFound<StudentResponseDto>($"Student with RowId {request.Student.RowId} not found.");
            }

            _mapper.Map(request.Student, entity);
            await _repository.UpdateAsync(entity);
            var result = _mapper.Map<StudentResponseDto>(entity);
            return Result.Ok(result, "Student updated successfully");
        }
    }
    #endregion

    #region delete
    public class DeleteStudentCommand : IRequest<CustomActionResult<bool>>
    {
        public int Id { get; set; }
    }

    public class DeleteStudentHandler : IRequestHandler<DeleteStudentCommand, CustomActionResult<bool>>
    {
        private readonly IGenericRepository<StudentEntity> _repository;

        public DeleteStudentHandler(IGenericRepository<StudentEntity> repository)
        {
            _repository = repository;
        }

        public async Task<CustomActionResult<bool>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return new CustomActionResult<bool> { Data = false, ResponseType = 404, ResponseDesc = "Student not found", IsSuccess = false };

            await _repository.DeleteAsync(request.Id);
            return Result.Ok<bool>(true, "Student deleted successfully");
        }
    }
    #endregion
}
