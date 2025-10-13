using AutoMapper;
using Example.Domain.Entities;
using Example.Service.Models.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Raya.Hrm.Shared.Library.GeneralRepository;
using Raya.Hrm.Shared.Library.Models;
using Raya.Hrm.Shared.Library.Models.Base;
using Raya.Hrm.Shared.Library.Utilities;

namespace Example.Service.Handler.Queries.ScoreForm
{
    public class GetScoreFormById : IRequest<ScoreFormResponseDto?>
    {
        public int Id { get; set; }
    }

    public class GetScoreFormByIdHandler : IRequestHandler<GetScoreFormById, ScoreFormResponseDto?>
    {
        private readonly IGenericRepository<ScoreFormEntity> _repository;
        private readonly IMapper _mapper;

        public GetScoreFormByIdHandler(IGenericRepository<ScoreFormEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ScoreFormResponseDto?> Handle(GetScoreFormById request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return null;

            return _mapper.Map<ScoreFormResponseDto>(entity);
        }
    }

    public class GetAllScoreFormsQuery : IRequest<CustomActionResult<List<ScoreFormResponseDto>>>
    {
    }

    public class GetAllScoreFormsHandler : IRequestHandler<GetAllScoreFormsQuery, CustomActionResult<List<ScoreFormResponseDto>>>
    {
        private readonly IGenericRepository<ScoreFormEntity> _repository;
        private readonly IMapper _mapper;

        public GetAllScoreFormsHandler(IGenericRepository<ScoreFormEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomActionResult<List<ScoreFormResponseDto>>> Handle(GetAllScoreFormsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();
            var result = _mapper.Map<List<ScoreFormResponseDto>>(entities);
            return Result.Ok(result, "Score forms retrieved successfully").WithTotalCount(result.Count);
        }
    }

    public class GetFilteredScoreFormsQuery : IRequest<CustomActionResult<List<ScoreFormResponseDto>>>
    {
        public FilterPagedListParameter<ScoreFormEntity>? Filters { get; set; }

        public GetFilteredScoreFormsQuery()
        {
        }

        public GetFilteredScoreFormsQuery(FilterPagedListParameter<ScoreFormEntity>? filters)
        {
            Filters = filters;
        }
    }

    public class GetFilteredScoreFormsHandler : IRequestHandler<GetFilteredScoreFormsQuery, CustomActionResult<List<ScoreFormResponseDto>>>
    {
        private readonly IGenericRepository<ScoreFormEntity> _repository;
        private readonly IMapper _mapper;

        public GetFilteredScoreFormsHandler(IGenericRepository<ScoreFormEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomActionResult<List<ScoreFormResponseDto>>> Handle(GetFilteredScoreFormsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<ScoreFormEntity> entities;
            if (request.Filters.IsNull())
            {
                var all = await _repository.GetAllAsync();
                var dto = _mapper.Map<List<ScoreFormResponseDto>>(all);
                return Result.Ok(dto, "Score forms retrieved successfully").WithTotalCount(dto.Count);
            }
            else
            {
                var filtered = await _repository.GetFilteredAsync(request.Filters);
                var dto = _mapper.Map<List<ScoreFormResponseDto>>(filtered);
                return Result.Ok(dto, "Score forms retrieved successfully").WithTotalCount(dto.Count);
            }
        }
    }
}