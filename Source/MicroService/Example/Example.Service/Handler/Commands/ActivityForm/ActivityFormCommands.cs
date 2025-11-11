using AutoMapper;
using Example.Domain.Entities;
using Example.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;
using Raya.Hrm.Shared.Library.Models.Base;
using Raya.Hrm.Shared.Library.Utilities;

namespace Example.Service.Handler.Commands.ActivityForm
{
    #region create
    public class CreateActivityFormCommand : IRequest<CustomActionResult<ActivityFormResponseDto>>
    {
        public DateOnly ActivityDate { get; set; }
        public long SelectedProgramId { get; set; }
        public long SelectedStudentId { get; set; }
    }

    public class CreateActivityFormHandler : IRequestHandler<CreateActivityFormCommand, CustomActionResult<ActivityFormResponseDto>>
    {
        private readonly IGenericRepository<ActivityFormEntity> _repository;
        private readonly IMapper _mapper;

        public CreateActivityFormHandler(IGenericRepository<ActivityFormEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomActionResult<ActivityFormResponseDto>> Handle(CreateActivityFormCommand request, CancellationToken cancellationToken)
        {
            var entity = new ActivityFormEntity
            {
                ActivityDate = request.ActivityDate,
                SelectedProgramId = request.SelectedProgramId,
                SelectedStudentId = request.SelectedStudentId
            };

            var savedEntity = await _repository.AddAsync(entity);
            var result = _mapper.Map<ActivityFormResponseDto>(savedEntity);
            return Result.Created(result, "Activity form created successfully");
        }
    }
    #endregion

    #region update
    public class UpdateActivityFormCommand : IRequest<CustomActionResult<ActivityFormResponseDto>>
    {
        public ActivityFormUpdateModel ActivityForm { get; set; }

        public UpdateActivityFormCommand()
        {
        }

        public UpdateActivityFormCommand(ActivityFormUpdateModel activityForm)
        {
            ActivityForm = activityForm;
        }
    }

    public class UpdateActivityFormCommandHandler : IRequestHandler<UpdateActivityFormCommand, CustomActionResult<ActivityFormResponseDto>>
    {
        private readonly IGenericRepository<ActivityFormEntity> _repository;
        private readonly IMapper _mapper;

        public UpdateActivityFormCommandHandler(IGenericRepository<ActivityFormEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomActionResult<ActivityFormResponseDto>> Handle(UpdateActivityFormCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.ActivityForm.RowId.Value);
            if (entity.IsNull())
            {
                return Result.NotFound<ActivityFormResponseDto>($"Activity form with RowId {request.ActivityForm.RowId} not found.");
            }

            _mapper.Map(request.ActivityForm, entity);
            await _repository.UpdateAsync(entity);
            var result = _mapper.Map<ActivityFormResponseDto>(entity);
            return Result.Ok(result, "Activity form updated successfully");
        }
    }
    #endregion

    #region delete
    public class DeleteActivityFormCommand : IRequest<CustomActionResult<bool>>
    {
        public int Id { get; set; }
    }

    public class DeleteActivityFormHandler : IRequestHandler<DeleteActivityFormCommand, CustomActionResult<bool>>
    {
        private readonly IGenericRepository<ActivityFormEntity> _repository;

        public DeleteActivityFormHandler(IGenericRepository<ActivityFormEntity> repository)
        {
            _repository = repository;
        }

        public async Task<CustomActionResult<bool>> Handle(DeleteActivityFormCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return new CustomActionResult<bool> { Data = false, ResponseType = 404, ResponseDesc = "Activity form not found", IsSuccess = false };

            await _repository.DeleteAsync(request.Id);
            return Result.Ok<bool>(true, "Activity form deleted successfully");
        }
    }
    #endregion
}