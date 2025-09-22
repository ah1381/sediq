using AutoMapper;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;

namespace LoanManagement.Service.Handler.Queries.FundRegion
{
    public class GetFundRegionByIdQuery : IRequest<FundRegionDto?>
    {
        public long Id { get; set; }

        public GetFundRegionByIdQuery(long id)
        {
            Id = id;
        }
    }

    public class GetFundRegionByIdHandler : IRequestHandler<GetFundRegionByIdQuery, FundRegionDto?>
    {
        private readonly IGenericRepository<FundRegionEntity> _repository;
        private readonly IMapper _mapper;

        public GetFundRegionByIdHandler(IGenericRepository<FundRegionEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FundRegionDto?> Handle(GetFundRegionByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return null;

            return _mapper.Map<FundRegionDto>(entity);
        }
    }

    public class GetAllFundRegionsQuery : IRequest<List<FundRegionDto>>
    {
    }

    public class GetAllFundRegionsHandler : IRequestHandler<GetAllFundRegionsQuery, List<FundRegionDto>>
    {
        private readonly IGenericRepository<FundRegionEntity> _repository;
        private readonly IMapper _mapper;

        public GetAllFundRegionsHandler(IGenericRepository<FundRegionEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FundRegionDto>> Handle(GetAllFundRegionsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<FundRegionDto>>(entities);
        }
    }
}