using AutoMapper;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;

namespace LoanManagement.Service.Handler.Queries.LoanRequestLog
{
    public class GetLoanRequestLogByIdQuery : IRequest<LoanRequestLogDto?>
    {
        public long Id { get; set; }

        public GetLoanRequestLogByIdQuery(long id)
        {
            Id = id;
        }
    }

    public class GetLoanRequestLogByIdHandler : IRequestHandler<GetLoanRequestLogByIdQuery, LoanRequestLogDto?>
    {
        private readonly IGenericRepository<LoanRequestLogEntity> _repository;
        private readonly IMapper _mapper;

        public GetLoanRequestLogByIdHandler(IGenericRepository<LoanRequestLogEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<LoanRequestLogDto?> Handle(GetLoanRequestLogByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return null;

            return _mapper.Map<LoanRequestLogDto>(entity);
        }
    }

    public class GetAllLoanRequestLogsQuery : IRequest<List<LoanRequestLogDto>>
    {
    }

    public class GetAllLoanRequestLogsHandler : IRequestHandler<GetAllLoanRequestLogsQuery, List<LoanRequestLogDto>>
    {
        private readonly IGenericRepository<LoanRequestLogEntity> _repository;
        private readonly IMapper _mapper;

        public GetAllLoanRequestLogsHandler(IGenericRepository<LoanRequestLogEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<LoanRequestLogDto>> Handle(GetAllLoanRequestLogsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<LoanRequestLogDto>>(entities);
        }
    }
}