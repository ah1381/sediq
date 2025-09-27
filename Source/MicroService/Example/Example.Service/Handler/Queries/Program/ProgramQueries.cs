using AutoMapper;
using Example.Domain.Entities;
using Example.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;

namespace Example.Service.Handler.Queries.Program
{



        public class GetProgramQuery : IRequest<ProgramResponseDto?>
        {
            public int Id { get; set; }
        }

        public class GetProgramHandler : IRequestHandler<GetProgramQuery, ProgramResponseDto?>
        {
            private readonly IGenericRepository<ProgramEntity> _repository;
            private readonly IMapper _mapper;

            public GetProgramHandler(IGenericRepository<ProgramEntity> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<ProgramResponseDto?> Handle(GetProgramQuery request, CancellationToken cancellationToken)
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                if (entity == null)
                    return null;

                return _mapper.Map<ProgramResponseDto>(entity);
            }
        }
    }

