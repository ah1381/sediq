using AutoMapper;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;

namespace LoanManagement.Service.Handler.Queries.FundHierarchy
{
    public class GetFundHierarchyByIdQuery : IRequest<FundHierarchyDto?>
    {
        public long Id { get; set; }

        public GetFundHierarchyByIdQuery(long id)
        {
            Id = id;
        }
    }

    public class GetFundHierarchyByIdHandler : IRequestHandler<GetFundHierarchyByIdQuery, FundHierarchyDto?>
    {
        private readonly IGenericRepository<FundHierarchyEntity> _repository;
        private readonly IMapper _mapper;

        public GetFundHierarchyByIdHandler(IGenericRepository<FundHierarchyEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FundHierarchyDto?> Handle(GetFundHierarchyByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return null;

            return _mapper.Map<FundHierarchyDto>(entity);
        }
    }

    public class GetAllFundHierarchiesQuery : IRequest<List<FundHierarchyDto>>
    {
    }

    public class GetAllFundHierarchiesHandler : IRequestHandler<GetAllFundHierarchiesQuery, List<FundHierarchyDto>>
    {
        private readonly IGenericRepository<FundHierarchyEntity> _repository;
        private readonly IMapper _mapper;

        public GetAllFundHierarchiesHandler(IGenericRepository<FundHierarchyEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FundHierarchyDto>> Handle(GetAllFundHierarchiesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<FundHierarchyDto>>(entities);
        }
    }
}