using AutoMapper;
using Example.Domain.Entities;
using Example.Service.Models.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Raya.Hrm.Shared.Library.GeneralRepository;

namespace Example.Service.Handler.Queries.Student
{
        public class GetStudentQuery : IRequest<StudentResponseDto?>
        {
            public int Id { get; set; }
        }

        public class GetStudentHandler : IRequestHandler<GetStudentQuery, StudentResponseDto?>
        {
            private readonly IGenericRepository<StudentEntity> _repository;
            private readonly IMapper _mapper;

            public GetStudentHandler(IGenericRepository<StudentEntity> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<StudentResponseDto?> Handle(GetStudentQuery request, CancellationToken cancellationToken)
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                if (entity == null)
                    return null;

                return _mapper.Map<StudentResponseDto>(entity);
            }
        }

        public class GetAllStudentsQuery : IRequest<IEnumerable<StudentResponseDto>>
        {
            public int PageNumber { get; set; } = 1;
            public int PageSize { get; set; } = 10;
        }

        public class GetAllStudentsHandler : IRequestHandler<GetAllStudentsQuery, IEnumerable<StudentResponseDto>>
        {
            private readonly IGenericRepository<StudentEntity> _repository;
            private readonly IMapper _mapper;

            public GetAllStudentsHandler(IGenericRepository<StudentEntity> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<IEnumerable<StudentResponseDto>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
            {
                var entities = await _repository.GetQueryable()
                    .Include(s => s.PhoneNumbers)
                    .Include(s => s.Photos)
                    .ToListAsync(cancellationToken);
                return _mapper.Map<IEnumerable<StudentResponseDto>>(entities);
            }
        }

        public class GetStudentByCodeQuery : IRequest<StudentResponseDto?>
        {
            public int StudentCode { get; set; }
        }

        public class GetStudentByCodeHandler : IRequestHandler<GetStudentByCodeQuery, StudentResponseDto?>
        {
            private readonly IGenericRepository<StudentEntity> _repository;
            private readonly IMapper _mapper;

            public GetStudentByCodeHandler(IGenericRepository<StudentEntity> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<StudentResponseDto?> Handle(GetStudentByCodeQuery request, CancellationToken cancellationToken)
            {
                var entity = await _repository.GetQueryable()
                    .Include(s => s.PhoneNumbers)
                    .Include(s => s.Photos)
                    .FirstOrDefaultAsync(s => s.StudentCode == request.StudentCode, cancellationToken);
                
                return entity == null ? null : _mapper.Map<StudentResponseDto>(entity);
            }
        }
    }

