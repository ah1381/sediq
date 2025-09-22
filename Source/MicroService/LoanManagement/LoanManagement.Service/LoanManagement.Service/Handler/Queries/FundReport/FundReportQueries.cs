using AutoMapper;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;

namespace LoanManagement.Service.Handler.Queries.FundReport
{
    public class GetFundReportByIdQuery : IRequest<FundReportDto?>
    {
        public long Id { get; set; }

        public GetFundReportByIdQuery(long id)
        {
            Id = id;
        }
    }

    public class GetFundReportByIdHandler : IRequestHandler<GetFundReportByIdQuery, FundReportDto?>
    {
        private readonly IGenericRepository<FundReportEntity> _repository;
        private readonly IMapper _mapper;

        public GetFundReportByIdHandler(IGenericRepository<FundReportEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FundReportDto?> Handle(GetFundReportByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return null;

            return _mapper.Map<FundReportDto>(entity);
        }
    }

    public class GetAllFundReportsQuery : IRequest<List<FundReportDto>>
    {
    }

    public class GetAllFundReportsHandler : IRequestHandler<GetAllFundReportsQuery, List<FundReportDto>>
    {
        private readonly IGenericRepository<FundReportEntity> _repository;
        private readonly IMapper _mapper;

        public GetAllFundReportsHandler(IGenericRepository<FundReportEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FundReportDto>> Handle(GetAllFundReportsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<FundReportDto>>(entities);
        }
    }
}