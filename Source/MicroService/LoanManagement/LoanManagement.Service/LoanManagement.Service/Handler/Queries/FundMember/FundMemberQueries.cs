using AutoMapper;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;

namespace LoanManagement.Service.Handler.Queries.FundMember
{
    public class GetFundMemberByIdQuery : IRequest<FundMemberDto?>
    {
        public long Id { get; set; }

        public GetFundMemberByIdQuery(long id)
        {
            Id = id;
        }
    }

    public class GetFundMemberByIdHandler : IRequestHandler<GetFundMemberByIdQuery, FundMemberDto?>
    {
        private readonly IGenericRepository<FundMemberEntity> _repository;
        private readonly IMapper _mapper;

        public GetFundMemberByIdHandler(IGenericRepository<FundMemberEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FundMemberDto?> Handle(GetFundMemberByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return null;

            return _mapper.Map<FundMemberDto>(entity);
        }
    }

    public class GetAllFundMembersQuery : IRequest<List<FundMemberDto>>
    {
    }

    public class GetAllFundMembersHandler : IRequestHandler<GetAllFundMembersQuery, List<FundMemberDto>>
    {
        private readonly IGenericRepository<FundMemberEntity> _repository;
        private readonly IMapper _mapper;

        public GetAllFundMembersHandler(IGenericRepository<FundMemberEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FundMemberDto>> Handle(GetAllFundMembersQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<FundMemberDto>>(entities);
        }
    }
}
