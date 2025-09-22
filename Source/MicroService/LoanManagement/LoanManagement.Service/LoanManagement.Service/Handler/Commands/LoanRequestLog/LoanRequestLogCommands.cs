using LoanManagement.Domain.Data;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LoanManagement.Service.Handler.Commands.LoanRequestLog
{
    public class CreateLoanRequestLogCommand : IRequest<LoanRequestLogDto>
    {
        public LoanRequestLogDto LoanRequestLog { get; set; }

        public CreateLoanRequestLogCommand(LoanRequestLogDto loanRequestLog)
        {
            LoanRequestLog = loanRequestLog;
        }
    }

    public class CreateLoanRequestLogCommandHandler : IRequestHandler<CreateLoanRequestLogCommand, LoanRequestLogDto>
    {
        private readonly AppDbContext _context;

        public CreateLoanRequestLogCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LoanRequestLogDto> Handle(CreateLoanRequestLogCommand request, CancellationToken cancellationToken)
        {
            var entity = new LoanRequestLogEntity
            {
                LoanRequestID = request.LoanRequestLog.LoanRequestId,
                Action = request.LoanRequestLog.Action,
                ActionBy = request.LoanRequestLog.ActionBy,
                ActionDate = request.LoanRequestLog.ActionDate,
                Note = request.LoanRequestLog.Note,
                CreatedAt = DateTime.UtcNow,
                RevSeq = 1,
                Status = request.LoanRequestLog.Status,
                RandId = request.LoanRequestLog.RandId
            };

            _context.LoanRequestLogs.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            request.LoanRequestLog.RowId = entity.RowId;
            return request.LoanRequestLog;
        }
    }

    public class UpdateLoanRequestLogCommand : IRequest<LoanRequestLogDto>
    {
        public LoanRequestLogDto LoanRequestLog { get; set; }

        public UpdateLoanRequestLogCommand(LoanRequestLogDto loanRequestLog)
        {
            LoanRequestLog = loanRequestLog;
        }
    }

    public class UpdateLoanRequestLogCommandHandler : IRequestHandler<UpdateLoanRequestLogCommand, LoanRequestLogDto>
    {
        private readonly AppDbContext _context;

        public UpdateLoanRequestLogCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LoanRequestLogDto> Handle(UpdateLoanRequestLogCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.LoanRequestLogs
                .FirstOrDefaultAsync(lrl => lrl.RowId == request.LoanRequestLog.RowId, cancellationToken);

            if (entity == null)
            {
                throw new KeyNotFoundException($"LoanRequestLog with RowId {request.LoanRequestLog.RowId} not found.");
            }

            entity.LoanRequestID = request.LoanRequestLog.LoanRequestId;
            entity.Action = request.LoanRequestLog.Action;
            entity.ActionBy = request.LoanRequestLog.ActionBy;
            entity.ActionDate = request.LoanRequestLog.ActionDate;
            entity.Note = request.LoanRequestLog.Note;
            entity.Status = request.LoanRequestLog.Status;
            entity.RandId = request.LoanRequestLog.RandId;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.RevSeq++;

            await _context.SaveChangesAsync(cancellationToken);

            return request.LoanRequestLog;
        }
    }

    public class DeleteLoanRequestLogCommand : IRequest<bool>
    {
        public int RowId { get; set; }

        public DeleteLoanRequestLogCommand(int rowId)
        {
            RowId = rowId;
        }
    }

    public class DeleteLoanRequestLogCommandHandler : IRequestHandler<DeleteLoanRequestLogCommand, bool>
    {
        private readonly AppDbContext _context;

        public DeleteLoanRequestLogCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteLoanRequestLogCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.LoanRequestLogs
                .FirstOrDefaultAsync(lrl => lrl.RowId == request.RowId, cancellationToken);

            if (entity == null)
            {
                return false;
            }

            _context.LoanRequestLogs.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}