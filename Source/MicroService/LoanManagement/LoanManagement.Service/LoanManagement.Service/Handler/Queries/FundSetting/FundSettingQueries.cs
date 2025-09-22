using AutoMapper;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;

namespace LoanManagement.Service.Handler.Queries.FundSetting
{
    public class GetFundSettingByIdQuery : IRequest<FundSettingDto?>
    {
        public long Id { get; set; }

        public GetFundSettingByIdQuery(long id)
        {
            Id = id;
        }
    }

    public class GetFundSettingByIdHandler : IRequestHandler<GetFundSettingByIdQuery, FundSettingDto?>
    {
        private readonly IGenericRepository<FundSettingEntity> _repository;
        private readonly IMapper _mapper;

        public GetFundSettingByIdHandler(IGenericRepository<FundSettingEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FundSettingDto?> Handle(GetFundSettingByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return null;

            return _mapper.Map<FundSettingDto>(entity);
        }
    }

    public class GetAllFundSettingsQuery : IRequest<List<FundSettingDto>>
    {
    }

    public class GetAllFundSettingsHandler : IRequestHandler<GetAllFundSettingsQuery, List<FundSettingDto>>
    {
        private readonly IGenericRepository<FundSettingEntity> _repository;
        private readonly IMapper _mapper;

        public GetAllFundSettingsHandler(IGenericRepository<FundSettingEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FundSettingDto>> Handle(GetAllFundSettingsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<FundSettingDto>>(entities);
        }
    }
}
