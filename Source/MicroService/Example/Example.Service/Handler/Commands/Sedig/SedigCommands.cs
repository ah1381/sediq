using AutoMapper;
using Example.Domain.Entities;
using Example.Service.Models.DTOs;
using MediatR;
using Raya.Hrm.Shared.Library.GeneralRepository;
using Raya.Hrm.Shared.Library.Models.Base;
using Raya.Hrm.Shared.Library.Utilities;

namespace Example.Service.Handler.Commands.sediq
{
    #region create
    public class CreatesediqCommand : IRequest<CustomActionResult<sediqResponseDto>>
    {
        public long sediqCode { get; set; }
        public string sediqName { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public string? Description { get; set; } = string.Empty;
    }

    public class CreatesediqHandler : IRequestHandler<CreatesediqCommand, CustomActionResult<sediqResponseDto>>
    {
        private readonly IGenericRepository<SediqEntity> _repository;
        private readonly IMapper _mapper;

        public CreatesediqHandler(IGenericRepository<SediqEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomActionResult<sediqResponseDto>> Handle(CreatesediqCommand request, CancellationToken cancellationToken)
        {
            var entity = new SediqEntity
            {
                SediqCode = request.sediqCode,
                sediqName = request.sediqName,
                StartDate = request.StartDate,
                Description = request.Description
            };
            var saveEntity = await _repository.AddAsync(entity);
            var result = _mapper.Map<sediqResponseDto>(saveEntity);
            return Result.Created(result, "sediq created successfully");
        }
    }
    #endregion

    #region update
    public class UpdatesediqCommand : IRequest<CustomActionResult<sediqResponseDto>>
    {
        public sediqUpdateModel sediq { get; set; }

        public UpdatesediqCommand()
        {
        }

        public UpdatesediqCommand(sediqUpdateModel sediq)
        {
            this.sediq = sediq;
        }
    }

    public class UpdatesediqCommandHandler : IRequestHandler<UpdatesediqCommand, CustomActionResult<sediqResponseDto>>
    {
        private readonly IGenericRepository<SediqEntity> _repository;
        private readonly IMapper _mapper;

        public UpdatesediqCommandHandler(IGenericRepository<SediqEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomActionResult<sediqResponseDto>> Handle(UpdatesediqCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.sediq.RowId.Value);
            if (entity.IsNull())
            {
                return Result.NotFound<sediqResponseDto>($"sediq with RowId {request.sediq.RowId} not found.");
            }

            _mapper.Map(request.sediq, entity);
            await _repository.UpdateAsync(entity);
            var result = _mapper.Map<sediqResponseDto>(entity);
            return Result.Ok(result, "sediq updated successfully");
        }
    }
    #endregion

    #region delete
    public class DeletesediqCommand : IRequest<CustomActionResult<bool>>
    {
        public int Id { get; set; }
    }

    public class DeletesediqHandler : IRequestHandler<DeletesediqCommand, CustomActionResult<bool>>
    {
        private readonly IGenericRepository<SediqEntity> _repository;

        public DeletesediqHandler(IGenericRepository<SediqEntity> repository)
        {
            _repository = repository;
        }

        public async Task<CustomActionResult<bool>> Handle(DeletesediqCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return new CustomActionResult<bool> { Data = false, ResponseType = 404, ResponseDesc = "sediq not found", IsSuccess = false };

            await _repository.DeleteAsync(request.Id);
            return Result.Ok<bool>(true, "sediq deleted successfully");
        }
    }
    #endregion
}