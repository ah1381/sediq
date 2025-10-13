using AutoMapper;
using Example.Domain.Entities;
using Example.Service.Models.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Raya.Hrm.Shared.Library.GeneralRepository;
using Raya.Hrm.Shared.Library.Models;
using Raya.Hrm.Shared.Library.Models.Base;
using Raya.Hrm.Shared.Library.Utilities;

namespace Example.Service.Handler.Queries.ActivityForm
{
    public class GetActivityFormById : IRequest<ActivityFormResponseDto?>
    {
        public int Id { get; set; }
    }

    public class GetActivityFormByIdHandler : IRequestHandler<GetActivityFormById, ActivityFormResponseDto?>
    {
        private readonly IGenericRepository<ActivityFormEntity> _repository;
        private readonly IMapper _mapper;

        public GetActivityFormByIdHandler(IGenericRepository<ActivityFormEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ActivityFormResponseDto?> Handle(GetActivityFormById request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return null;

            return _mapper.Map<ActivityFormResponseDto>(entity);
        }
    }

    public class GetAllActivityFormsQuery : IRequest<CustomActionResult<List<ActivityFormResponseDto>>>
    {
    }

    public class GetAllActivityFormsHandler : IRequestHandler<GetAllActivityFormsQuery, CustomActionResult<List<ActivityFormResponseDto>>>
    {
        private readonly IGenericRepository<ActivityFormEntity> _repository;
        private readonly IMapper _mapper;

        public GetAllActivityFormsHandler(IGenericRepository<ActivityFormEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomActionResult<List<ActivityFormResponseDto>>> Handle(GetAllActivityFormsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();
            var result = _mapper.Map<List<ActivityFormResponseDto>>(entities);
            return Result.Ok(result, "Activity forms retrieved successfully").WithTotalCount(result.Count);
        }
    }

    public class GetFilteredActivityFormsQuery : IRequest<CustomActionResult<List<ActivityFormResponseDto>>>
    {
        public FilterPagedListParameter<ActivityFormEntity>? Filters { get; set; }

        public GetFilteredActivityFormsQuery()
        {
        }

        public GetFilteredActivityFormsQuery(FilterPagedListParameter<ActivityFormEntity>? filters)
        {
            Filters = filters;
        }
    }

    public class GetFilteredActivityFormsHandler : IRequestHandler<GetFilteredActivityFormsQuery, CustomActionResult<List<ActivityFormResponseDto>>>
    {
        private readonly IGenericRepository<ActivityFormEntity> _repository;
        private readonly IMapper _mapper;

        public GetFilteredActivityFormsHandler(IGenericRepository<ActivityFormEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomActionResult<List<ActivityFormResponseDto>>> Handle(GetFilteredActivityFormsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<ActivityFormEntity> entities;
            if (request.Filters.IsNull())
            {
                var all = await _repository.GetAllAsync();
                var dto = _mapper.Map<List<ActivityFormResponseDto>>(all);
                return Result.Ok(dto, "Activity forms retrieved successfully").WithTotalCount(dto.Count);
            }
            else
            {
                var filtered = await _repository.GetFilteredAsync(request.Filters);
                var dto = _mapper.Map<List<ActivityFormResponseDto>>(filtered);
                return Result.Ok(dto, "Activity forms retrieved successfully").WithTotalCount(dto.Count);
            }
        }
    }
}