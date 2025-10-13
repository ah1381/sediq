using AutoMapper;
using Example.Domain.Entities;
using Example.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;
using Raya.Hrm.Shared.Library.Models;
using Raya.Hrm.Shared.Library.Models.Base;
using Raya.Hrm.Shared.Library.Utilities;

namespace Example.Service.Handler.Queries.Program
{
    public class GetProgramById : IRequest<ProgramResponseDto?>
    {
        public int Id { get; set; }
    }

    public class GetProgramByIdHandler : IRequestHandler<GetProgramById, ProgramResponseDto?>
    {
        private readonly IGenericRepository<ProgramEntity> _repository;
        private readonly IMapper _mapper;

        public GetProgramByIdHandler(IGenericRepository<ProgramEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProgramResponseDto?> Handle(GetProgramById request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return null;

            return _mapper.Map<ProgramResponseDto>(entity);
        }
    }

    public class GetAllProgramsQuery : IRequest<CustomActionResult<List<ProgramResponseDto>>>
    {
    }

    public class GetAllProgramsHandler : IRequestHandler<GetAllProgramsQuery, CustomActionResult<List<ProgramResponseDto>>>
    {
        private readonly IGenericRepository<ProgramEntity> _repository;
        private readonly IMapper _mapper;

        public GetAllProgramsHandler(IGenericRepository<ProgramEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomActionResult<List<ProgramResponseDto>>> Handle(GetAllProgramsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();
            var result = _mapper.Map<List<ProgramResponseDto>>(entities);
            return Result.Ok(result, "Programs retrieved successfully").WithTotalCount(result.Count);
        }
    }

    public class GetFilteredProgramsQuery : IRequest<CustomActionResult<List<ProgramResponseDto>>>
    {
        public FilterPagedListParameter<ProgramEntity>? Filters { get; set; }

        public GetFilteredProgramsQuery()
        {
        }

        public GetFilteredProgramsQuery(FilterPagedListParameter<ProgramEntity>? filters)
        {
            Filters = filters;
        }
    }

    public class GetFilteredProgramsHandler : IRequestHandler<GetFilteredProgramsQuery, CustomActionResult<List<ProgramResponseDto>>>
    {
        private readonly IGenericRepository<ProgramEntity> _repository;
        private readonly IMapper _mapper;

        public GetFilteredProgramsHandler(IGenericRepository<ProgramEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomActionResult<List<ProgramResponseDto>>> Handle(GetFilteredProgramsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<ProgramEntity> entities;
            if (request.Filters.IsNull())
            {
                var all = await _repository.GetAllAsync();
                var dto = _mapper.Map<List<ProgramResponseDto>>(all);
                return Result.Ok(dto, "Programs retrieved successfully").WithTotalCount(dto.Count);
            }
            else
            {
                var filtered = await _repository.GetFilteredAsync(request.Filters);
                var dto = _mapper.Map<List<ProgramResponseDto>>(filtered);
                return Result.Ok(dto, "Programs retrieved successfully").WithTotalCount(dto.Count);
            }
        }
    }
}

