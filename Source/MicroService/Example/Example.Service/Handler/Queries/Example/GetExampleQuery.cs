using AutoMapper;
using Example.Domain.Entities;
using Example.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;

namespace Example.Service.Handler.Queries.Example
{
    public class GetExampleQuery : IRequest<ExampleDto?>
    {
        public int Id { get; set; }
    }

    public class GetExampleHandler : IRequestHandler<GetExampleQuery, ExampleDto?>
    {
        private readonly IGenericRepository<ExampleEntity> _repository;
        private readonly IMapper _mapper;

        public GetExampleHandler(IGenericRepository<ExampleEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ExampleDto?> Handle(GetExampleQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return null;

            return _mapper.Map<ExampleDto>(entity);
        }
    }
}