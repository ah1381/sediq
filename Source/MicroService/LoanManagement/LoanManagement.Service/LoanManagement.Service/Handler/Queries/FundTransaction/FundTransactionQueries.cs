using AutoMapper;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;

namespace LoanManagement.Service.Handler.Queries.FundTransaction
{
    public class GetFundTransactionByIdQuery : IRequest<FundTransactionDto?>
    {
        public long Id { get; set; }

        public GetFundTransactionByIdQuery(long id)
        {
            Id = id;
        }
    }

    public class GetFundTransactionByIdHandler : IRequestHandler<GetFundTransactionByIdQuery, FundTransactionDto?>
    {
        private readonly IGenericRepository<FundTransactionEntity> _repository;
        private readonly IMapper _mapper;

        public GetFundTransactionByIdHandler(IGenericRepository<FundTransactionEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FundTransactionDto?> Handle(GetFundTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return null;

            return _mapper.Map<FundTransactionDto>(entity);
        }
    }

    public class GetAllFundTransactionsQuery : IRequest<List<FundTransactionDto>>
    {
    }

    public class GetAllFundTransactionsHandler : IRequestHandler<GetAllFundTransactionsQuery, List<FundTransactionDto>>
    {
        private readonly IGenericRepository<FundTransactionEntity> _repository;
        private readonly IMapper _mapper;

        public GetAllFundTransactionsHandler(IGenericRepository<FundTransactionEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FundTransactionDto>> Handle(GetAllFundTransactionsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<FundTransactionDto>>(entities);
        }
    }
}
