using AutoMapper;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;

namespace LoanManagement.Service.Handler.Queries.LoanCertificate
{
    public class GetLoanCertificateByIdQuery : IRequest<LoanCertificateDto?>
    {
        public long Id { get; set; }

        public GetLoanCertificateByIdQuery(long id)
        {
            Id = id;
        }
    }

    public class GetLoanCertificateByIdHandler : IRequestHandler<GetLoanCertificateByIdQuery, LoanCertificateDto?>
    {
        private readonly IGenericRepository<LoanCertificateEntity> _repository;
        private readonly IMapper _mapper;

        public GetLoanCertificateByIdHandler(IGenericRepository<LoanCertificateEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<LoanCertificateDto?> Handle(GetLoanCertificateByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return null;

            return _mapper.Map<LoanCertificateDto>(entity);
        }
    }

    public class GetAllLoanCertificatesQuery : IRequest<List<LoanCertificateDto>>
    {
    }

    public class GetAllLoanCertificatesHandler : IRequestHandler<GetAllLoanCertificatesQuery, List<LoanCertificateDto>>
    {
        private readonly IGenericRepository<LoanCertificateEntity> _repository;
        private readonly IMapper _mapper;

        public GetAllLoanCertificatesHandler(IGenericRepository<LoanCertificateEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<LoanCertificateDto>> Handle(GetAllLoanCertificatesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<LoanCertificateDto>>(entities);
        }
    }
}
