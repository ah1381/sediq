using Example.Domain.Entities;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;

namespace Example.Service.Handler.Commands.Example
{
    // Now returns long (inserted RowId)
    public class CreateExampleCommand : IRequest<long>
    {
        public string Name { get; set; } = string.Empty;
    }

    public class CreateExampleHandler : IRequestHandler<CreateExampleCommand, long>
    {
        private readonly IGenericRepository<ExampleEntity> _repository;

        public CreateExampleHandler(IGenericRepository<ExampleEntity> repository)
        {
            _repository = repository;
        }

        public async Task<long> Handle(CreateExampleCommand request, CancellationToken cancellationToken)
        {
            var entity = new ExampleEntity
            {
                Name = request.Name
                // BaseEntity properties (CreatedAt, Status, etc.) are handled by GenericRepository
            };

            return await _repository.AddAsync(entity); // return new RowId
        }
    }

    public class DeleteExampleCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteExampleHandler : IRequestHandler<DeleteExampleCommand>
    {
        private readonly IGenericRepository<ExampleEntity> _repository;

        public DeleteExampleHandler(IGenericRepository<ExampleEntity> repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteExampleCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id);
        }
    }
}
