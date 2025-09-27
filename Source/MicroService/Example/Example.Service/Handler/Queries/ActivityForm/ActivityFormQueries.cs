using AutoMapper;
using Example.Domain.Entities;
using Example.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;

namespace Example.Service.Handler.Queries.ActivityForm
{
    public class GetActivityFormQuery : IRequest<ActivityFormResponseDto?>
    {
        public int Id { get; set; }
    }

    public class GetActivityFormHandler : IRequestHandler<GetActivityFormQuery, ActivityFormResponseDto?>
    {
        private readonly IGenericRepository<ProgramEntity> _repository;
        private readonly IMapper _mapper;

        public GetActivityFormHandler(IGenericRepository<ProgramEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ActivityFormResponseDto?> Handle(GetActivityFormQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return null;

            return _mapper.Map<ActivityFormResponseDto>(entity);
        }
    }
}

