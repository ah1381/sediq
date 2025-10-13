using AutoMapper;
using Example.Domain.Entities;
using Example.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;
using Raya.Hrm.Shared.Library.Models.Base;
using Raya.Hrm.Shared.Library.Utilities;

namespace Example.Service.Handler.Commands.Program
{
    #region create
    public class CreateProgramCommand : IRequest<CustomActionResult<ProgramResponseDto>>
    {
        public string Name { get; set; } = string.Empty;
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }

    public class CreateProgramHandler : IRequestHandler<CreateProgramCommand, CustomActionResult<ProgramResponseDto>>
    {
        private readonly IGenericRepository<ProgramEntity> _repository;
        private readonly IMapper _mapper;

        public CreateProgramHandler(IGenericRepository<ProgramEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomActionResult<ProgramResponseDto>> Handle(CreateProgramCommand request, CancellationToken cancellationToken)
        {
            var entity = new ProgramEntity
            {
                Name = request.Name,
                From = request.From,
                To = request.To
            };
            var saveEntity = await _repository.AddAsync(entity);
            var result = _mapper.Map<ProgramResponseDto>(saveEntity);
            return Result.Created(result, "Program created successfully");
        }
    }
    #endregion

    #region update
    public class UpdateProgramCommand : IRequest<CustomActionResult<ProgramResponseDto>>
    {
        public ProgramUpdateModel Program { get; set; }

        public UpdateProgramCommand()
        {
        }

        public UpdateProgramCommand(ProgramUpdateModel program)
        {
            Program = program;
        }
    }

    public class UpdateProgramCommandHandler : IRequestHandler<UpdateProgramCommand, CustomActionResult<ProgramResponseDto>>
    {
        private readonly IGenericRepository<ProgramEntity> _repository;
        private readonly IMapper _mapper;

        public UpdateProgramCommandHandler(IGenericRepository<ProgramEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomActionResult<ProgramResponseDto>> Handle(UpdateProgramCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Program.RowId.Value);
            if (entity.IsNull())
            {
                return Result.NotFound<ProgramResponseDto>($"Program with RowId {request.Program.RowId} not found.");
            }

            _mapper.Map(request.Program, entity);
            await _repository.UpdateAsync(entity);
            var result = _mapper.Map<ProgramResponseDto>(entity);
            return Result.Ok(result, "Program updated successfully");
        }
    }
    #endregion

    #region delete
    public class DeleteProgramCommand : IRequest<CustomActionResult<bool>>
    {
        public int Id { get; set; }
    }

    public class DeleteProgramHandler : IRequestHandler<DeleteProgramCommand, CustomActionResult<bool>>
    {
        private readonly IGenericRepository<ProgramEntity> _repository;

        public DeleteProgramHandler(IGenericRepository<ProgramEntity> repository)
        {
            _repository = repository;
        }

        public async Task<CustomActionResult<bool>> Handle(DeleteProgramCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return new CustomActionResult<bool> { Data = false, ResponseType = 404, ResponseDesc = "Program not found", IsSuccess = false };

            await _repository.DeleteAsync(request.Id);
            return Result.Ok<bool>(true, "Program deleted successfully");
        }
    }
    #endregion
}
