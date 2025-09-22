using AutoMapper;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;

namespace LoanManagement.Service.Handler.Queries.LoanAdjustmentRequest
{
    public class GetLoanAdjustmentRequestByIdQuery : IRequest<LoanAdjustmentRequestDto?>
    {
        public long Id { get; set; }

        public GetLoanAdjustmentRequestByIdQuery(long id)
        {
            Id = id;
        }
    }

    public class GetLoanAdjustmentRequestByIdHandler : IRequestHandler<GetLoanAdjustmentRequestByIdQuery, LoanAdjustmentRequestDto?>
    {
        private readonly IGenericRepository<LoanAdjustmentRequestEntity> _repository;
        private readonly IMapper _mapper;

        public GetLoanAdjustmentRequestByIdHandler(IGenericRepository<LoanAdjustmentRequestEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<LoanAdjustmentRequestDto?> Handle(GetLoanAdjustmentRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return null;

            return _mapper.Map<LoanAdjustmentRequestDto>(entity);
        }
    }

    public class GetAllLoanAdjustmentRequestsQuery : IRequest<List<LoanAdjustmentRequestDto>>
    {
    }

    public class GetAllLoanAdjustmentRequestsHandler : IRequestHandler<GetAllLoanAdjustmentRequestsQuery, List<LoanAdjustmentRequestDto>>
    {
        private readonly IGenericRepository<LoanAdjustmentRequestEntity> _repository;
        private readonly IMapper _mapper;

        public GetAllLoanAdjustmentRequestsHandler(IGenericRepository<LoanAdjustmentRequestEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<LoanAdjustmentRequestDto>> Handle(GetAllLoanAdjustmentRequestsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<LoanAdjustmentRequestDto>>(entities);
        }
    }
}
