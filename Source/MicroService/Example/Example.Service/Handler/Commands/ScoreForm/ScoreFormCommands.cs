using AutoMapper;
using Example.Domain.Entities;
using Example.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;
using Raya.Hrm.Shared.Library.Models.Base;
using Raya.Hrm.Shared.Library.Utilities;

namespace Example.Service.Handler.Commands.ScoreForm
{
    #region create
    public class CreateScoreFormCommand : IRequest<CustomActionResult<ScoreFormResponseDto>>
    {
        public long SelectedProgramId { get; set; }
        public long SelectedStudentId { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Score { get; set; }
        public long ActivityDurId { get; set; }
    }

    public class CreateScoreFormHandler : IRequestHandler<CreateScoreFormCommand, CustomActionResult<ScoreFormResponseDto>>
    {
        private readonly IGenericRepository<ScoreFormEntity> _repository;
        private readonly IMapper _mapper;

        public CreateScoreFormHandler(IGenericRepository<ScoreFormEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomActionResult<ScoreFormResponseDto>> Handle(CreateScoreFormCommand request, CancellationToken cancellationToken)
        {
            var entity = new ScoreFormEntity
            {
                SelectedProgramId = request.SelectedProgramId,
                SelectedStudentId = request.SelectedStudentId,
                Description = request.Description,
                Score = request.Score,
                ActivityDurId = request.ActivityDurId
            };
            var saveEntity = await _repository.AddAsync(entity);
            var result = _mapper.Map<ScoreFormResponseDto>(saveEntity);
            return Result.Created(result, "Score form created successfully");
        }
    }
    #endregion

    #region update
    public class UpdateScoreFormCommand : IRequest<CustomActionResult<ScoreFormResponseDto>>
    {
        public ScoreFormUpdateModel ScoreForm { get; set; }

        public UpdateScoreFormCommand()
        {
        }

        public UpdateScoreFormCommand(ScoreFormUpdateModel scoreForm)
        {
            ScoreForm = scoreForm;
        }
    }

    public class UpdateScoreFormCommandHandler : IRequestHandler<UpdateScoreFormCommand, CustomActionResult<ScoreFormResponseDto>>
    {
        private readonly IGenericRepository<ScoreFormEntity> _repository;
        private readonly IMapper _mapper;

        public UpdateScoreFormCommandHandler(IGenericRepository<ScoreFormEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomActionResult<ScoreFormResponseDto>> Handle(UpdateScoreFormCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.ScoreForm.RowId.Value);
            if (entity.IsNull())
            {
                return Result.NotFound<ScoreFormResponseDto>($"Score form with RowId {request.ScoreForm.RowId} not found.");
            }

            _mapper.Map(request.ScoreForm, entity);
            await _repository.UpdateAsync(entity);
            var result = _mapper.Map<ScoreFormResponseDto>(entity);
            return Result.Ok(result, "Score form updated successfully");
        }
    }
    #endregion

    #region delete
    public class DeleteScoreFormCommand : IRequest<CustomActionResult<bool>>
    {
        public int Id { get; set; }
    }

    public class DeleteScoreFormHandler : IRequestHandler<DeleteScoreFormCommand, CustomActionResult<bool>>
    {
        private readonly IGenericRepository<ScoreFormEntity> _repository;

        public DeleteScoreFormHandler(IGenericRepository<ScoreFormEntity> repository)
        {
            _repository = repository;
        }

        public async Task<CustomActionResult<bool>> Handle(DeleteScoreFormCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return new CustomActionResult<bool> { Data = false, ResponseType = 404, ResponseDesc = "Score form not found", IsSuccess = false };

            await _repository.DeleteAsync(request.Id);
            return Result.Ok<bool>(true, "Score form deleted successfully");
        }
    }
    #endregion
}