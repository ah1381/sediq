using AutoMapper;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;

namespace LoanManagement.Service.Handler.Queries.FundElection
{
    public class GetFundElectionByIdQuery : IRequest<FundElectionDto?>
    {
        public long Id { get; set; }

        public GetFundElectionByIdQuery(long id)
        {
            Id = id;
        }
    }

    public class GetFundElectionByIdHandler : IRequestHandler<GetFundElectionByIdQuery, FundElectionDto?>
    {
        private readonly IGenericRepository<FundElectionEntity> _repository;
        private readonly IMapper _mapper;

        public GetFundElectionByIdHandler(IGenericRepository<FundElectionEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FundElectionDto?> Handle(GetFundElectionByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return null;

            return _mapper.Map<FundElectionDto>(entity);
        }
    }

    public class GetAllFundElectionsQuery : IRequest<List<FundElectionDto>>
    {
    }

    public class GetAllFundElectionsHandler : IRequestHandler<GetAllFundElectionsQuery, List<FundElectionDto>>
    {
        private readonly IGenericRepository<FundElectionEntity> _repository;
        private readonly IMapper _mapper;

        public GetAllFundElectionsHandler(IGenericRepository<FundElectionEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FundElectionDto>> Handle(GetAllFundElectionsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<FundElectionDto>>(entities);
        }
    }
}
