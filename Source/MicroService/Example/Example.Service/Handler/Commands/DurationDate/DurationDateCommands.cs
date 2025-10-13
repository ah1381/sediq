using AutoMapper;
using Example.Domain.Entities;
using Example.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;
using Raya.Hrm.Shared.Library.Models.Base;
using Raya.Hrm.Shared.Library.Utilities;

namespace Example.Service.Handler.Commands.DurationDate
{
    #region create
    public class CreateDurationDateCommand : IRequest<CustomActionResult<DurationDateEntityResponseDto>>
    {
        public DateOnly StartDur { get; set; }
        public DateOnly EndDur { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class CreateDurationDateHandler : IRequestHandler<CreateDurationDateCommand, CustomActionResult<DurationDateEntityResponseDto>>
    {
        private readonly IGenericRepository<DurationDateEntity> _repository;
        private readonly IMapper _mapper;

        public CreateDurationDateHandler(IGenericRepository<DurationDateEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomActionResult<DurationDateEntityResponseDto>> Handle(CreateDurationDateCommand request, CancellationToken cancellationToken)
        {
            var entity = new DurationDateEntity
            {
                StartDur = request.StartDur,
                EndDur = request.EndDur,
                Name = request.Name
            };
            var saveEntity = await _repository.AddAsync(entity);
            var result = _mapper.Map<DurationDateEntityResponseDto>(saveEntity);
            return Result.Created(result, "Duration date created successfully");
        }
    }
    #endregion

    #region update
    public class UpdateDurationDateCommand : IRequest<CustomActionResult<DurationDateEntityResponseDto>>
    {
        public DurationDateEntityUpdateModel DurationDate { get; set; }

        public UpdateDurationDateCommand()
        {
        }

        public UpdateDurationDateCommand(DurationDateEntityUpdateModel durationDate)
        {
            DurationDate = durationDate;
        }
    }

    public class UpdateDurationDateCommandHandler : IRequestHandler<UpdateDurationDateCommand, CustomActionResult<DurationDateEntityResponseDto>>
    {
        private readonly IGenericRepository<DurationDateEntity> _repository;
        private readonly IMapper _mapper;

        public UpdateDurationDateCommandHandler(IGenericRepository<DurationDateEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomActionResult<DurationDateEntityResponseDto>> Handle(UpdateDurationDateCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.DurationDate.RowId.Value);
            if (entity.IsNull())
            {
                return Result.NotFound<DurationDateEntityResponseDto>($"Duration date with RowId {request.DurationDate.RowId} not found.");
            }

            _mapper.Map(request.DurationDate, entity);
            await _repository.UpdateAsync(entity);
            var result = _mapper.Map<DurationDateEntityResponseDto>(entity);
            return Result.Ok(result, "Duration date updated successfully");
        }
    }
    #endregion

    #region delete
    public class DeleteDurationDateCommand : IRequest<CustomActionResult<bool>>
    {
        public int Id { get; set; }
    }

    public class DeleteDurationDateHandler : IRequestHandler<DeleteDurationDateCommand, CustomActionResult<bool>>
    {
        private readonly IGenericRepository<DurationDateEntity> _repository;

        public DeleteDurationDateHandler(IGenericRepository<DurationDateEntity> repository)
        {
            _repository = repository;
        }

        public async Task<CustomActionResult<bool>> Handle(DeleteDurationDateCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return new CustomActionResult<bool> { Data = false, ResponseType = 404, ResponseDesc = "Duration date not found", IsSuccess = false };

            await _repository.DeleteAsync(request.Id);
            return Result.Ok<bool>(true, "Duration date deleted successfully");
        }
    }
    #endregion
}