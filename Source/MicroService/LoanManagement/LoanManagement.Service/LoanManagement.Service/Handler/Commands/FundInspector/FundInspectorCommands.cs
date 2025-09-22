using LoanManagement.Domain.Data;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LoanManagement.Service.Handler.Commands.FundInspector
{
    public class CreateFundInspectorCommand : IRequest<FundInspectorDto>
    {
        public FundInspectorDto FundInspector { get; set; }

        public CreateFundInspectorCommand(FundInspectorDto fundInspector)
        {
            FundInspector = fundInspector;
        }
    }

    public class CreateFundInspectorCommandHandler : IRequestHandler<CreateFundInspectorCommand, FundInspectorDto>
    {
        private readonly AppDbContext _context;

        public CreateFundInspectorCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FundInspectorDto> Handle(CreateFundInspectorCommand request, CancellationToken cancellationToken)
        {
            var entity = new FundInspectorEntity
            {
                FundID = request.FundInspector.FundId,
                PersonnelID = request.FundInspector.PersonnelId,
                StartDate = request.FundInspector.StartDate,
                EndDate = request.FundInspector.EndDate,
                ReportFrequency = request.FundInspector.ReportFrequency,
                CreatedBy = request.FundInspector.CreatedBy,
                CreatedAt = DateTime.UtcNow,
                RevSeq = 1,
                Status = request.FundInspector.Status,
                RandId = request.FundInspector.RandId
            };

            _context.FundInspectors.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            request.FundInspector.RowId = entity.RowId;
            return request.FundInspector;
        }
    }

    public class UpdateFundInspectorCommand : IRequest<FundInspectorDto>
    {
        public FundInspectorDto FundInspector { get; set; }

        public UpdateFundInspectorCommand(FundInspectorDto fundInspector)
        {
            FundInspector = fundInspector;
        }
    }

    public class UpdateFundInspectorCommandHandler : IRequestHandler<UpdateFundInspectorCommand, FundInspectorDto>
    {
        private readonly AppDbContext _context;

        public UpdateFundInspectorCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FundInspectorDto> Handle(UpdateFundInspectorCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FundInspectors
                .FirstOrDefaultAsync(fi => fi.RowId == request.FundInspector.RowId, cancellationToken);

            if (entity == null)
            {
                throw new KeyNotFoundException($"FundInspector with RowId {request.FundInspector.RowId} not found.");
            }

            entity.FundID = request.FundInspector.FundId;
            entity.PersonnelID = request.FundInspector.PersonnelId;
            entity.StartDate = request.FundInspector.StartDate;
            entity.EndDate = request.FundInspector.EndDate;
            entity.ReportFrequency = request.FundInspector.ReportFrequency;
            entity.CreatedBy = request.FundInspector.CreatedBy;
            entity.Status = request.FundInspector.Status;
            entity.RandId = request.FundInspector.RandId;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.RevSeq++;

            await _context.SaveChangesAsync(cancellationToken);

            return request.FundInspector;
        }
    }

    public class DeleteFundInspectorCommand : IRequest<bool>
    {
        public int RowId { get; set; }

        public DeleteFundInspectorCommand(int rowId)
        {
            RowId = rowId;
        }
    }

    public class DeleteFundInspectorCommandHandler : IRequestHandler<DeleteFundInspectorCommand, bool>
    {
        private readonly AppDbContext _context;

        public DeleteFundInspectorCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteFundInspectorCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FundInspectors
                .FirstOrDefaultAsync(fi => fi.RowId == request.RowId, cancellationToken);

            if (entity == null)
            {
                return false;
            }

            _context.FundInspectors.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}