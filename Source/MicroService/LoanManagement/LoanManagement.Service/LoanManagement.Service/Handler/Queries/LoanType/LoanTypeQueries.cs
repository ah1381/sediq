using AutoMapper;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;

namespace LoanManagement.Service.Handler.Queries.LoanType
{
    public class GetLoanTypeByIdQuery : IRequest<LoanTypeDto?>
    {
        public long Id { get; set; }

        public GetLoanTypeByIdQuery(long id)
        {
            Id = id;
        }
    }

    public class GetLoanTypeByIdHandler : IRequestHandler<GetLoanTypeByIdQuery, LoanTypeDto?>
    {
        private readonly IGenericRepository<LoanTypeEntity> _repository;
        private readonly IMapper _mapper;

        public GetLoanTypeByIdHandler(IGenericRepository<LoanTypeEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<LoanTypeDto?> Handle(GetLoanTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return null;

            return _mapper.Map<LoanTypeDto>(entity);
        }
    }

    public class GetAllLoanTypesQuery : IRequest<List<LoanTypeDto>>
    {
    }

    public class GetAllLoanTypesHandler : IRequestHandler<GetAllLoanTypesQuery, List<LoanTypeDto>>
    {
        private readonly IGenericRepository<LoanTypeEntity> _repository;
        private readonly IMapper _mapper;

        public GetAllLoanTypesHandler(IGenericRepository<LoanTypeEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<LoanTypeDto>> Handle(GetAllLoanTypesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<LoanTypeDto>>(entities);
        }
    }
}
