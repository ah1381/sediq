using AutoMapper;
using Example.Domain.Entities;
using Example.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;
using Raya.Hrm.Shared.Library.Models;
using Raya.Hrm.Shared.Library.Models.Base;
using Raya.Hrm.Shared.Library.Utilities;

namespace Example.Service.Handler.Queries.DurationDate
{
    public class GetDurationDateById : IRequest<DurationDateEntityResponseDto?>
    {
        public int Id { get; set; }
    }

    public class GetDurationDateByIdHandler : IRequestHandler<GetDurationDateById, DurationDateEntityResponseDto?>
    {
        private readonly IGenericRepository<DurationDateEntity> _repository;
        private readonly IMapper _mapper;

        public GetDurationDateByIdHandler(IGenericRepository<DurationDateEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DurationDateEntityResponseDto?> Handle(GetDurationDateById request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return null;

            return _mapper.Map<DurationDateEntityResponseDto>(entity);
        }
    }

    public class GetAllDurationDatesQuery : IRequest<CustomActionResult<List<DurationDateEntityResponseDto>>>
    {
    }

    public class GetAllDurationDatesHandler : IRequestHandler<GetAllDurationDatesQuery, CustomActionResult<List<DurationDateEntityResponseDto>>>
    {
        private readonly IGenericRepository<DurationDateEntity> _repository;
        private readonly IMapper _mapper;

        public GetAllDurationDatesHandler(IGenericRepository<DurationDateEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomActionResult<List<DurationDateEntityResponseDto>>> Handle(GetAllDurationDatesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();
            var result = _mapper.Map<List<DurationDateEntityResponseDto>>(entities);
            return Result.Ok(result, "Duration dates retrieved successfully").WithTotalCount(result.Count);
        }
    }

    public class GetFilteredDurationDatesQuery : IRequest<CustomActionResult<List<DurationDateEntityResponseDto>>>
    {
        public FilterPagedListParameter<DurationDateEntity>? Filters { get; set; }

        public GetFilteredDurationDatesQuery()
        {
        }

        public GetFilteredDurationDatesQuery(FilterPagedListParameter<DurationDateEntity>? filters)
        {
            Filters = filters;
        }
    }

    public class GetFilteredDurationDatesHandler : IRequestHandler<GetFilteredDurationDatesQuery, CustomActionResult<List<DurationDateEntityResponseDto>>>
    {
        private readonly IGenericRepository<DurationDateEntity> _repository;
        private readonly IMapper _mapper;

        public GetFilteredDurationDatesHandler(IGenericRepository<DurationDateEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomActionResult<List<DurationDateEntityResponseDto>>> Handle(GetFilteredDurationDatesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<DurationDateEntity> entities;
            if (request.Filters.IsNull())
            {
                var all = await _repository.GetAllAsync();
                var dto = _mapper.Map<List<DurationDateEntityResponseDto>>(all);
                return Result.Ok(dto, "Duration dates retrieved successfully").WithTotalCount(dto.Count);
            }
            else
            {
                var filtered = await _repository.GetFilteredAsync(request.Filters);
                var dto = _mapper.Map<List<DurationDateEntityResponseDto>>(filtered);
                return Result.Ok(dto, "Duration dates retrieved successfully").WithTotalCount(dto.Count);
            }
        }
    }
}