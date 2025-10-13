using AutoMapper;
using Example.Domain.Entities;
using Example.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;
using Raya.Hrm.Shared.Library.Models;
using Raya.Hrm.Shared.Library.Models.Base;
using Raya.Hrm.Shared.Library.Utilities;

namespace Example.Service.Handler.Queries.sediq
{
    public class GetsediqById : IRequest<sediqResponseDto?>
    {
        public int Id { get; set; }
    }

    public class GetsediqByIdHandler : IRequestHandler<GetsediqById, sediqResponseDto?>
    {
        private readonly IGenericRepository<SediqEntity> _repository;
        private readonly IMapper _mapper;

        public GetsediqByIdHandler(IGenericRepository<SediqEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<sediqResponseDto?> Handle(GetsediqById request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return null;

            return _mapper.Map<sediqResponseDto>(entity);
        }
    }

    public class GetAllsediqsQuery : IRequest<CustomActionResult<List<sediqResponseDto>>>
    {
    }

    public class GetAllsediqsHandler : IRequestHandler<GetAllsediqsQuery, CustomActionResult<List<sediqResponseDto>>>
    {
        private readonly IGenericRepository<SediqEntity> _repository;
        private readonly IMapper _mapper;

        public GetAllsediqsHandler(IGenericRepository<SediqEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomActionResult<List<sediqResponseDto>>> Handle(GetAllsediqsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();
            var result = _mapper.Map<List<sediqResponseDto>>(entities);
            return Result.Ok(result, "sediqs retrieved successfully").WithTotalCount(result.Count);
        }
    }

    public class GetFilteredsediqsQuery : IRequest<CustomActionResult<List<sediqResponseDto>>>
    {
        public FilterPagedListParameter<SediqEntity>? Filters { get; set; }

        public GetFilteredsediqsQuery()
        {
        }

        public GetFilteredsediqsQuery(FilterPagedListParameter<SediqEntity>? filters)
        {
            Filters = filters;
        }
    }

    public class GetFilteredsediqsHandler : IRequestHandler<GetFilteredsediqsQuery, CustomActionResult<List<sediqResponseDto>>>
    {
        private readonly IGenericRepository<SediqEntity> _repository;
        private readonly IMapper _mapper;

        public GetFilteredsediqsHandler(IGenericRepository<SediqEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomActionResult<List<sediqResponseDto>>> Handle(GetFilteredsediqsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<SediqEntity> entities;
            if (request.Filters.IsNull())
            {
                var all = await _repository.GetAllAsync();
                var dto = _mapper.Map<List<sediqResponseDto>>(all);
                return Result.Ok(dto, "sediqs retrieved successfully").WithTotalCount(dto.Count);
            }
            else
            {
                var filtered = await _repository.GetFilteredAsync(request.Filters);
                var dto = _mapper.Map<List<sediqResponseDto>>(filtered);
                return Result.Ok(dto, "sediqs retrieved successfully").WithTotalCount(dto.Count);
            }
        }
    }
}