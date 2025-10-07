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

    public class GetAllProgramsQuery : IRequest<IEnumerable<ProgramResponseDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetAllProgramsHandler : IRequestHandler<GetAllProgramsQuery, IEnumerable<ProgramResponseDto>>
    {
        private readonly IGenericRepository<ProgramEntity> _repository;
        private readonly IMapper _mapper;

        public GetAllProgramsHandler(IGenericRepository<ProgramEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProgramResponseDto>> Handle(GetAllProgramsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProgramResponseDto>>(entities);
        }
    }

}

