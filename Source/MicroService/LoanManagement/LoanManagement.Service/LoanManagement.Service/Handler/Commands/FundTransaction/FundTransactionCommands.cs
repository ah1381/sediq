using LoanManagement.Domain.Data;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LoanManagement.Service.Handler.Commands.FundTransaction
{
    public class CreateFundTransactionCommand : IRequest<FundTransactionDto>
    {
        public FundTransactionDto FundTransaction { get; set; }

        public CreateFundTransactionCommand(FundTransactionDto fundTransaction)
        {
            FundTransaction = fundTransaction;
        }
    }

    public class CreateFundTransactionCommandHandler : IRequestHandler<CreateFundTransactionCommand, FundTransactionDto>
    {
        private readonly AppDbContext _context;

        public CreateFundTransactionCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FundTransactionDto> Handle(CreateFundTransactionCommand request, CancellationToken cancellationToken)
        {
            var entity = new FundTransactionEntity
            {
                FundID = request.FundTransaction.FundId,
                MemberID = request.FundTransaction.MemberId,
                TransactionDate = request.FundTransaction.TransactionDate,
                TransactionType = request.FundTransaction.TransactionType,
                Amount = request.FundTransaction.Amount,
                Balance = request.FundTransaction.Balance,
                Description = request.FundTransaction.Description,
                CreatedBy = request.FundTransaction.CreatedBy,
                DebitAmount = request.FundTransaction.DebitAmount,
                CreditAmount = request.FundTransaction.CreditAmount,
                Commission = request.FundTransaction.Commission,
                Insurance = request.FundTransaction.Insurance,
                CreatedAt = DateTime.UtcNow,
                RevSeq = 1,
                Status = request.FundTransaction.Status,
                RandId = request.FundTransaction.RandId
            };

            _context.FundTransactions.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            request.FundTransaction.RowId = entity.RowId;
            return request.FundTransaction;
        }
    }

    public class UpdateFundTransactionCommand : IRequest<FundTransactionDto>
    {
        public FundTransactionDto FundTransaction { get; set; }

        public UpdateFundTransactionCommand(FundTransactionDto fundTransaction)
        {
            FundTransaction = fundTransaction;
        }
    }

    public class UpdateFundTransactionCommandHandler : IRequestHandler<UpdateFundTransactionCommand, FundTransactionDto>
    {
        private readonly AppDbContext _context;

        public UpdateFundTransactionCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FundTransactionDto> Handle(UpdateFundTransactionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FundTransactions
                .FirstOrDefaultAsync(ft => ft.RowId == request.FundTransaction.RowId, cancellationToken);

            if (entity == null)
            {
                throw new KeyNotFoundException($"FundTransaction with RowId {request.FundTransaction.RowId} not found.");
            }

            entity.FundID = request.FundTransaction.FundId;
            entity.MemberID = request.FundTransaction.MemberId;
            entity.TransactionDate = request.FundTransaction.TransactionDate;
            entity.TransactionType = request.FundTransaction.TransactionType;
            entity.Amount = request.FundTransaction.Amount;
            entity.Balance = request.FundTransaction.Balance;
            entity.Description = request.FundTransaction.Description;
            entity.CreatedBy = request.FundTransaction.CreatedBy;
            entity.DebitAmount = request.FundTransaction.DebitAmount;
            entity.CreditAmount = request.FundTransaction.CreditAmount;
            entity.Commission = request.FundTransaction.Commission;
            entity.Insurance = request.FundTransaction.Insurance;
            entity.Status = request.FundTransaction.Status;
            entity.RandId = request.FundTransaction.RandId;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.RevSeq++;

            await _context.SaveChangesAsync(cancellationToken);

            return request.FundTransaction;
        }
    }

    public class DeleteFundTransactionCommand : IRequest<bool>
    {
        public int RowId { get; set; }

        public DeleteFundTransactionCommand(int rowId)
        {
            RowId = rowId;
        }
    }

    public class DeleteFundTransactionCommandHandler : IRequestHandler<DeleteFundTransactionCommand, bool>
    {
        private readonly AppDbContext _context;

        public DeleteFundTransactionCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteFundTransactionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FundTransactions
                .FirstOrDefaultAsync(ft => ft.RowId == request.RowId, cancellationToken);

            if (entity == null)
            {
                return false;
            }

            _context.FundTransactions.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}