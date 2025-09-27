using AutoMapper;
using Example.Domain.Entities;
using Example.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;

namespace Example.Service.Handler.Queries.Student
{
        public class GetStudentQuery : IRequest<StudentResponseDto?>
        {
            public int Id { get; set; }
        }

        public class GetStudentHandler : IRequestHandler<GetStudentQuery, StudentResponseDto?>
        {
            private readonly IGenericRepository<ProgramEntity> _repository;
            private readonly IMapper _mapper;

            public GetStudentHandler(IGenericRepository<ProgramEntity> repository, IMapper mapper)
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
    }

