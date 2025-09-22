using LoanManagement.Domain.Data;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LoanManagement.Service.Handler.Commands.FundTransfer
{
    public class CreateFundTransferCommand : IRequest<FundTransferDto>
    {
        public FundTransferDto FundTransfer { get; set; }

        public CreateFundTransferCommand(FundTransferDto fundTransfer)
        {
            FundTransfer = fundTransfer;
        }
    }

    public class CreateFundTransferCommandHandler : IRequestHandler<CreateFundTransferCommand, FundTransferDto>
    {
        private readonly AppDbContext _context;

        public CreateFundTransferCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FundTransferDto> Handle(CreateFundTransferCommand request, CancellationToken cancellationToken)
        {
            var entity = new FundTransferEntity
            {
                MemberID = request.FundTransfer.MemberId,
                FromFundID = request.FundTransfer.FromFundId,
                ToFundID = request.FundTransfer.ToFundId,
                TransferDate = request.FundTransfer.TransferDate,
                TransferType = request.FundTransfer.TransferType,
                Amount = request.FundTransfer.Amount,
                Description = request.FundTransfer.Description,
                CreatedBy = request.FundTransfer.CreatedBy,
                CreatedAt = DateTime.UtcNow,
                RevSeq = 1,
                Status = request.FundTransfer.Status,
                RandId = request.FundTransfer.RandId
            };

            _context.FundTransfers.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            request.FundTransfer.RowId = entity.RowId;
            return request.FundTransfer;
        }
    }

    public class UpdateFundTransferCommand : IRequest<FundTransferDto>
    {
        public FundTransferDto FundTransfer { get; set; }

        public UpdateFundTransferCommand(FundTransferDto fundTransfer)
        {
            FundTransfer = fundTransfer;
        }
    }

    public class UpdateFundTransferCommandHandler : IRequestHandler<UpdateFundTransferCommand, FundTransferDto>
    {
        private readonly AppDbContext _context;

        public UpdateFundTransferCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FundTransferDto> Handle(UpdateFundTransferCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FundTransfers
                .FirstOrDefaultAsync(ft => ft.RowId == request.FundTransfer.RowId, cancellationToken);

            if (entity == null)
            {
                throw new KeyNotFoundException($"FundTransfer with RowId {request.FundTransfer.RowId} not found.");
            }

            entity.MemberID = request.FundTransfer.MemberId;
            entity.FromFundID = request.FundTransfer.FromFundId;
            entity.ToFundID = request.FundTransfer.ToFundId;
            entity.TransferDate = request.FundTransfer.TransferDate;
            entity.TransferType = request.FundTransfer.TransferType;
            entity.Amount = request.FundTransfer.Amount;
            entity.Description = request.FundTransfer.Description;
            entity.CreatedBy = request.FundTransfer.CreatedBy;
            entity.Status = request.FundTransfer.Status;
            entity.RandId = request.FundTransfer.RandId;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.RevSeq++;

            await _context.SaveChangesAsync(cancellationToken);

            return request.FundTransfer;
        }
    }

    public class DeleteFundTransferCommand : IRequest<bool>
    {
        public int RowId { get; set; }

        public DeleteFundTransferCommand(int rowId)
        {
            RowId = rowId;
        }
    }

    public class DeleteFundTransferCommandHandler : IRequestHandler<DeleteFundTransferCommand, bool>
    {
        private readonly AppDbContext _context;

        public DeleteFundTransferCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteFundTransferCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FundTransfers
                .FirstOrDefaultAsync(ft => ft.RowId == request.RowId, cancellationToken);

            if (entity == null)
            {
                return false;
            }

            _context.FundTransfers.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
