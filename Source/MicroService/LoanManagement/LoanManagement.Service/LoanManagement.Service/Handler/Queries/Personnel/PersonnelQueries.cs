using AutoMapper;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;

namespace LoanManagement.Service.Handler.Queries.Personnel
{
    public class GetPersonnelByIdQuery : IRequest<PersonnelDto?>
    {
        public long Id { get; set; }

        public GetPersonnelByIdQuery(long id)
        {
            Id = id;
        }
    }

    public class GetPersonnelByIdHandler : IRequestHandler<GetPersonnelByIdQuery, PersonnelDto?>
    {
        private readonly IGenericRepository<PersonnelEntity> _repository;
        private readonly IMapper _mapper;

        public GetPersonnelByIdHandler(IGenericRepository<PersonnelEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PersonnelDto?> Handle(GetPersonnelByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return null;

            return _mapper.Map<PersonnelDto>(entity);
        }
    }

    public class GetAllPersonnelQuery : IRequest<List<PersonnelDto>>
    {
    }

    public class GetAllPersonnelHandler : IRequestHandler<GetAllPersonnelQuery, List<PersonnelDto>>
    {
        private readonly IGenericRepository<PersonnelEntity> _repository;
        private readonly IMapper _mapper;

        public GetAllPersonnelHandler(IGenericRepository<PersonnelEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<PersonnelDto>> Handle(GetAllPersonnelQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<PersonnelDto>>(entities);
        }
    }
}