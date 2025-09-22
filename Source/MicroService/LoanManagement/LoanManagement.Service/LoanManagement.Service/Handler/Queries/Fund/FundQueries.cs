using AutoMapper;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;

namespace LoanManagement.Service.Handler.Queries.Fund
{
    public class GetFundByIdQuery : IRequest<FundDto?>
    {
        public long Id { get; set; }

        public GetFundByIdQuery(long id)
        {
            Id = id;
        }
    }

    public class GetFundByIdHandler : IRequestHandler<GetFundByIdQuery, FundDto?>
    {
        private readonly IGenericRepository<FundEntity> _repository;
        private readonly IMapper _mapper;

        public GetFundByIdHandler(IGenericRepository<FundEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FundDto?> Handle(GetFundByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return null;

            return _mapper.Map<FundDto>(entity);
        }
    }

    public class GetAllFundsQuery : IRequest<List<FundDto>>
    {
    }

    public class GetAllFundsHandler : IRequestHandler<GetAllFundsQuery, List<FundDto>>
    {
        private readonly IGenericRepository<FundEntity> _repository;
        private readonly IMapper _mapper;

        public GetAllFundsHandler(IGenericRepository<FundEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FundDto>> Handle(GetAllFundsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<FundDto>>(entities);
        }
    }
}
