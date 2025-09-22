using AutoMapper;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagement.Service.Handler.Queries.FundInsurance
{
    public class GetFundInsuranceByIdQuery : IRequest<FundInsuranceDto?>
    {
        public long Id { get; set; }

        public GetFundInsuranceByIdQuery(long id)
        {
            Id = id;
        }
    }

    public class GetFundInsuranceByIdHandler : IRequestHandler<GetFundInsuranceByIdQuery, FundInsuranceDto?>
    {
        private readonly IGenericRepository<FundInsuranceEntity> _repository;
        private readonly IMapper _mapper;

        public GetFundInsuranceByIdHandler(IGenericRepository<FundInsuranceEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FundInsuranceDto?> Handle(GetFundInsuranceByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return null;

            return _mapper.Map<FundInsuranceDto>(entity);
        }
    }

    public class GetAllFundInsurancesQuery : IRequest<List<FundInsuranceDto>>
    {
    }

    public class GetAllFundInsurancesHandler : IRequestHandler<GetAllFundInsurancesQuery, List<FundInsuranceDto>>
    {
        private readonly IGenericRepository<FundInsuranceEntity> _repository;
        private readonly IMapper _mapper;

        public GetAllFundInsurancesHandler(IGenericRepository<FundInsuranceEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FundInsuranceDto>> Handle(GetAllFundInsurancesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<FundInsuranceDto>>(entities);
        }
    }
}
