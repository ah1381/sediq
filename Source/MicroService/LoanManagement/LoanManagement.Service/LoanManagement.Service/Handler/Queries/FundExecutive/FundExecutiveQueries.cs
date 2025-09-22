using AutoMapper;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;

namespace LoanManagement.Service.Handler.Queries.FundExecutive
{
    public class GetFundExecutiveByIdQuery : IRequest<FundExecutiveDto?>
    {
        public long Id { get; set; }

        public GetFundExecutiveByIdQuery(long id)
        {
            Id = id;
        }
    }

    public class GetFundExecutiveByIdHandler : IRequestHandler<GetFundExecutiveByIdQuery, FundExecutiveDto?>
    {
        private readonly IGenericRepository<FundExecutiveEntity> _repository;
        private readonly IMapper _mapper;

        public GetFundExecutiveByIdHandler(IGenericRepository<FundExecutiveEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FundExecutiveDto?> Handle(GetFundExecutiveByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return null;

            return _mapper.Map<FundExecutiveDto>(entity);
        }
    }

    public class GetAllFundExecutivesQuery : IRequest<List<FundExecutiveDto>>
    {
    }

    public class GetAllFundExecutivesHandler : IRequestHandler<GetAllFundExecutivesQuery, List<FundExecutiveDto>>
    {
        private readonly IGenericRepository<FundExecutiveEntity> _repository;
        private readonly IMapper _mapper;

        public GetAllFundExecutivesHandler(IGenericRepository<FundExecutiveEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FundExecutiveDto>> Handle(GetAllFundExecutivesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<FundExecutiveDto>>(entities);
        }
    }
}
