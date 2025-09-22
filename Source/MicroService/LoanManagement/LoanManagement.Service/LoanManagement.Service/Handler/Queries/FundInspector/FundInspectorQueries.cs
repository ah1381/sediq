using AutoMapper;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;

namespace LoanManagement.Service.Handler.Queries.FundInspector
{
    public class GetFundInspectorByIdQuery : IRequest<FundInspectorDto?>
    {
        public long Id { get; set; }

        public GetFundInspectorByIdQuery(long id)
        {
            Id = id;
        }
    }

    public class GetFundInspectorByIdHandler : IRequestHandler<GetFundInspectorByIdQuery, FundInspectorDto?>
    {
        private readonly IGenericRepository<FundInspectorEntity> _repository;
        private readonly IMapper _mapper;

        public GetFundInspectorByIdHandler(IGenericRepository<FundInspectorEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FundInspectorDto?> Handle(GetFundInspectorByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return null;

            return _mapper.Map<FundInspectorDto>(entity);
        }
    }

    public class GetAllFundInspectorsQuery : IRequest<List<FundInspectorDto>>
    {
    }

    public class GetAllFundInspectorsHandler : IRequestHandler<GetAllFundInspectorsQuery, List<FundInspectorDto>>
    {
        private readonly IGenericRepository<FundInspectorEntity> _repository;
        private readonly IMapper _mapper;

        public GetAllFundInspectorsHandler(IGenericRepository<FundInspectorEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FundInspectorDto>> Handle(GetAllFundInspectorsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<FundInspectorDto>>(entities);
        }
    }
}