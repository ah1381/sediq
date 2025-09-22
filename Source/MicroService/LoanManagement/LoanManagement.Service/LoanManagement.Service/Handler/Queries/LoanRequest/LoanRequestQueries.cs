using AutoMapper;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;

namespace LoanManagement.Service.Handler.Queries.LoanRequest
{
    public class GetLoanRequestByIdQuery : IRequest<LoanRequestDto?>
    {
        public long Id { get; set; }

        public GetLoanRequestByIdQuery(long id)
        {
            Id = id;
        }
    }

    public class GetLoanRequestByIdHandler : IRequestHandler<GetLoanRequestByIdQuery, LoanRequestDto?>
    {
        private readonly IGenericRepository<LoanRequestEntity> _repository;
        private readonly IMapper _mapper;

        public GetLoanRequestByIdHandler(IGenericRepository<LoanRequestEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<LoanRequestDto?> Handle(GetLoanRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return null;

            return _mapper.Map<LoanRequestDto>(entity);
        }
    }

    public class GetAllLoanRequestsQuery : IRequest<List<LoanRequestDto>>
    {
    }

    public class GetAllLoanRequestsHandler : IRequestHandler<GetAllLoanRequestsQuery, List<LoanRequestDto>>
    {
        private readonly IGenericRepository<LoanRequestEntity> _repository;
        private readonly IMapper _mapper;

        public GetAllLoanRequestsHandler(IGenericRepository<LoanRequestEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<LoanRequestDto>> Handle(GetAllLoanRequestsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<LoanRequestDto>>(entities);
        }
    }
}
