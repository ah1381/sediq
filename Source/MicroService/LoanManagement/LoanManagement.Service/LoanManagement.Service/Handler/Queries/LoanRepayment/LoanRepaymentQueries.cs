using AutoMapper;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;

namespace LoanManagement.Service.Handler.Queries.LoanRepayment
{
    public class GetLoanRepaymentByIdQuery : IRequest<LoanRepaymentDto?>
    {
        public long Id { get; set; }

        public GetLoanRepaymentByIdQuery(long id)
        {
            Id = id;
        }
    }

    public class GetLoanRepaymentByIdHandler : IRequestHandler<GetLoanRepaymentByIdQuery, LoanRepaymentDto?>
    {
        private readonly IGenericRepository<LoanRepaymentEntity> _repository;
        private readonly IMapper _mapper;

        public GetLoanRepaymentByIdHandler(IGenericRepository<LoanRepaymentEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<LoanRepaymentDto?> Handle(GetLoanRepaymentByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return null;

            return _mapper.Map<LoanRepaymentDto>(entity);
        }
    }

    public class GetAllLoanRepaymentsQuery : IRequest<List<LoanRepaymentDto>>
    {
    }

    public class GetAllLoanRepaymentsHandler : IRequestHandler<GetAllLoanRepaymentsQuery, List<LoanRepaymentDto>>
    {
        private readonly IGenericRepository<LoanRepaymentEntity> _repository;
        private readonly IMapper _mapper;

        public GetAllLoanRepaymentsHandler(IGenericRepository<LoanRepaymentEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<LoanRepaymentDto>> Handle(GetAllLoanRepaymentsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<LoanRepaymentDto>>(entities);
        }
    }
}
