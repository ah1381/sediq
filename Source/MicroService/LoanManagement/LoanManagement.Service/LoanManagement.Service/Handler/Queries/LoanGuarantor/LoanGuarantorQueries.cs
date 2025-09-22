using AutoMapper;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;

namespace LoanManagement.Service.Handler.Queries.LoanGuarantor
{
    public class GetLoanGuarantorByIdQuery : IRequest<LoanGuarantorDto?>
    {
        public long Id { get; set; }

        public GetLoanGuarantorByIdQuery(long id)
        {
            Id = id;
        }
    }

    public class GetLoanGuarantorByIdHandler : IRequestHandler<GetLoanGuarantorByIdQuery, LoanGuarantorDto?>
    {
        private readonly IGenericRepository<LoanGuarantorEntity> _repository;
        private readonly IMapper _mapper;

        public GetLoanGuarantorByIdHandler(IGenericRepository<LoanGuarantorEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<LoanGuarantorDto?> Handle(GetLoanGuarantorByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return null;

            return _mapper.Map<LoanGuarantorDto>(entity);
        }
    }

    public class GetAllLoanGuarantorsQuery : IRequest<List<LoanGuarantorDto>>
    {
    }

    public class GetAllLoanGuarantorsHandler : IRequestHandler<GetAllLoanGuarantorsQuery, List<LoanGuarantorDto>>
    {
        private readonly IGenericRepository<LoanGuarantorEntity> _repository;
        private readonly IMapper _mapper;

        public GetAllLoanGuarantorsHandler(IGenericRepository<LoanGuarantorEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<LoanGuarantorDto>> Handle(GetAllLoanGuarantorsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<LoanGuarantorDto>>(entities);
        }
    }
}