using AutoMapper;
using Example.Domain.Entities;
using Example.Service.Models.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Raya.Hrm.Shared.Library.GeneralRepository;
using Raya.Hrm.Shared.Library.Models;
using Raya.Hrm.Shared.Library.Models.Base;
using Raya.Hrm.Shared.Library.Utilities;

namespace Example.Service.Handler.Queries.Student
{
    public class GetStudentById : IRequest<StudentResponseDto?>
    {
        public int Id { get; set; }
    }

    public class GetStudentByIdHandler : IRequestHandler<GetStudentById, StudentResponseDto?>
    {
        private readonly IGenericRepository<StudentEntity> _repository;
        private readonly IMapper _mapper;

        public GetStudentByIdHandler(IGenericRepository<StudentEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<StudentResponseDto?> Handle(GetStudentById request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return null;

            return _mapper.Map<StudentResponseDto>(entity);
        }
    }

    public class GetAllStudentsQuery : IRequest<CustomActionResult<List<StudentResponseDto>>>
    {
    }

    public class GetAllStudentsHandler : IRequestHandler<GetAllStudentsQuery, CustomActionResult<List<StudentResponseDto>>>
    {
        private readonly IGenericRepository<StudentEntity> _repository;
        private readonly IMapper _mapper;

        public GetAllStudentsHandler(IGenericRepository<StudentEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomActionResult<List<StudentResponseDto>>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetQueryable()
                .Where(e => e.Status != 0)
                .Select(s => new StudentEntity
                {
                    RowId = s.RowId,
                    StudentCode = s.StudentCode,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    NationalCode = s.NationalCode,
                    MembershipDate = s.MembershipDate,
                    FatherName = s.FatherName,
                    FatherJob = s.FatherJob,
                    BirthDate = s.BirthDate,
                    FieldOfStudy = s.FieldOfStudy,
                    YearStudy = s.YearStudy,
                    Gender = s.Gender,
                    Address = s.Address,
                    EducationStatus = s.EducationStatus,
                    Notes = s.Notes,
                    CreatedAt = s.CreatedAt,
                    UpdatedAt = s.UpdatedAt,
                    Status = s.Status,
                    CreatedBy = s.CreatedBy
                })
                .ToListAsync();
            var result = _mapper.Map<List<StudentResponseDto>>(entities);
            return Result.Ok(result, "Students retrieved successfully").WithTotalCount(result.Count);
        }
    }

    public class GetFilteredStudentsQuery : IRequest<CustomActionResult<List<StudentResponseDto>>>
    {
        public FilterPagedListParameter<StudentEntity>? Filters { get; set; }

        public GetFilteredStudentsQuery()
        {
        }

        public GetFilteredStudentsQuery(FilterPagedListParameter<StudentEntity>? filters)
        {
            Filters = filters;
        }
    }

    public class GetFilteredStudentsHandler : IRequestHandler<GetFilteredStudentsQuery, CustomActionResult<List<StudentResponseDto>>>
    {
        private readonly IGenericRepository<StudentEntity> _repository;
        private readonly IMapper _mapper;

        public GetFilteredStudentsHandler(IGenericRepository<StudentEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomActionResult<List<StudentResponseDto>>> Handle(GetFilteredStudentsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<StudentEntity> entities;
            if (request.Filters.IsNull())
            {
                var all = await _repository.GetAllAsync();
                var dto = _mapper.Map<List<StudentResponseDto>>(all);
                return Result.Ok(dto, "Students retrieved successfully").WithTotalCount(dto.Count);
            }
            else
            {
                var filtered = await _repository.GetFilteredAsync(request.Filters);
                var dto = _mapper.Map<List<StudentResponseDto>>(filtered);
                return Result.Ok(dto, "Students retrieved successfully").WithTotalCount(dto.Count);
            }
        }
    }
}

