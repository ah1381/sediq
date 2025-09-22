using AutoMapper;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;

namespace LoanManagement.Service.Handler.Queries.FundTransfer
{
    public class GetFundTransferByIdQuery : IRequest<FundTransferDto?>
    {
        public long Id { get; set; }

        public GetFundTransferByIdQuery(long id)
        {
            Id = id;
        }
    }

    public class GetFundTransferByIdHandler : IRequestHandler<GetFundTransferByIdQuery, FundTransferDto?>
    {
        private readonly IGenericRepository<FundTransferEntity> _repository;
        private readonly IMapper _mapper;

        public GetFundTransferByIdHandler(IGenericRepository<FundTransferEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FundTransferDto?> Handle(GetFundTransferByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return null;

            return _mapper.Map<FundTransferDto>(entity);
        }
    }

    public class GetAllFundTransfersQuery : IRequest<List<FundTransferDto>>
    {
    }

    public class GetAllFundTransfersHandler : IRequestHandler<GetAllFundTransfersQuery, List<FundTransferDto>>
    {
        private readonly IGenericRepository<FundTransferEntity> _repository;
        private readonly IMapper _mapper;

        public GetAllFundTransfersHandler(IGenericRepository<FundTransferEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FundTransferDto>> Handle(GetAllFundTransfersQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<FundTransferDto>>(entities);
        }
    }
}