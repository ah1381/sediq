using LoanManagement.Domain.Data;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LoanManagement.Service.Handler.Commands.LoanAdjustmentRequest
{
    public class CreateLoanAdjustmentRequestCommand : IRequest<LoanAdjustmentRequestDto>
    {
        public LoanAdjustmentRequestDto LoanAdjustmentRequest { get; set; }

        public CreateLoanAdjustmentRequestCommand(LoanAdjustmentRequestDto loanAdjustmentRequest)
        {
            LoanAdjustmentRequest = loanAdjustmentRequest;
        }
    }

    public class CreateLoanAdjustmentRequestCommandHandler : IRequestHandler<CreateLoanAdjustmentRequestCommand, LoanAdjustmentRequestDto>
    {
        private readonly AppDbContext _context;

        public CreateLoanAdjustmentRequestCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LoanAdjustmentRequestDto> Handle(CreateLoanAdjustmentRequestCommand request, CancellationToken cancellationToken)
        {
            var entity = new LoanAdjustmentRequestEntity
            {
                MemberID = request.LoanAdjustmentRequest.MemberId,
                RequestType = request.LoanAdjustmentRequest.RequestType,
                OldValue = request.LoanAdjustmentRequest.OldValue,
                NewValue = request.LoanAdjustmentRequest.NewValue,
                MaxAllowed = request.LoanAdjustmentRequest.MaxAllowed,
                Description = request.LoanAdjustmentRequest.Description,
                RequestDate = request.LoanAdjustmentRequest.RequestDate,
                StatusDesc = request.LoanAdjustmentRequest.StatusDesc,
                CreatedBy = request.LoanAdjustmentRequest.CreatedBy,
                Online = request.LoanAdjustmentRequest.Online,
                CreatedAt = DateTime.UtcNow,
                RevSeq = 1,
                Status = request.LoanAdjustmentRequest.Status,
                RandId = request.LoanAdjustmentRequest.RandId
            };

            _context.LoanAdjustmentRequests.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            request.LoanAdjustmentRequest.RowId = entity.RowId;
            return request.LoanAdjustmentRequest;
        }
    }

    public class UpdateLoanAdjustmentRequestCommand : IRequest<LoanAdjustmentRequestDto>
    {
        public LoanAdjustmentRequestDto LoanAdjustmentRequest { get; set; }

        public UpdateLoanAdjustmentRequestCommand(LoanAdjustmentRequestDto loanAdjustmentRequest)
        {
            LoanAdjustmentRequest = loanAdjustmentRequest;
        }
    }

    public class UpdateLoanAdjustmentRequestCommandHandler : IRequestHandler<UpdateLoanAdjustmentRequestCommand, LoanAdjustmentRequestDto>
    {
        private readonly AppDbContext _context;

        public UpdateLoanAdjustmentRequestCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LoanAdjustmentRequestDto> Handle(UpdateLoanAdjustmentRequestCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.LoanAdjustmentRequests
                .FirstOrDefaultAsync(lar => lar.RowId == request.LoanAdjustmentRequest.RowId, cancellationToken);

            if (entity == null)
            {
                throw new KeyNotFoundException($"LoanAdjustmentRequest with RowId {request.LoanAdjustmentRequest.RowId} not found.");
            }

            entity.MemberID = request.LoanAdjustmentRequest.MemberId;
            entity.RequestType = request.LoanAdjustmentRequest.RequestType;
            entity.OldValue = request.LoanAdjustmentRequest.OldValue;
            entity.NewValue = request.LoanAdjustmentRequest.NewValue;
            entity.MaxAllowed = request.LoanAdjustmentRequest.MaxAllowed;
            entity.Description = request.LoanAdjustmentRequest.Description;
            entity.RequestDate = request.LoanAdjustmentRequest.RequestDate;
            entity.StatusDesc = request.LoanAdjustmentRequest.StatusDesc;
            entity.CreatedBy = request.LoanAdjustmentRequest.CreatedBy;
            entity.Online = request.LoanAdjustmentRequest.Online;
            entity.Status = request.LoanAdjustmentRequest.Status;
            entity.RandId = request.LoanAdjustmentRequest.RandId;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.RevSeq++;

            await _context.SaveChangesAsync(cancellationToken);

            return request.LoanAdjustmentRequest;
        }
    }

    public class DeleteLoanAdjustmentRequestCommand : IRequest<bool>
    {
        public int RowId { get; set; }

        public DeleteLoanAdjustmentRequestCommand(int rowId)
        {
            RowId = rowId;
        }
    }

    public class DeleteLoanAdjustmentRequestCommandHandler : IRequestHandler<DeleteLoanAdjustmentRequestCommand, bool>
    {
        private readonly AppDbContext _context;

        public DeleteLoanAdjustmentRequestCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteLoanAdjustmentRequestCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.LoanAdjustmentRequests
                .FirstOrDefaultAsync(lar => lar.RowId == request.RowId, cancellationToken);

            if (entity == null)
            {
                return false;
            }

            _context.LoanAdjustmentRequests.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}