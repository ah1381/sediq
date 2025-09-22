using LoanManagement.Domain.Data;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LoanManagement.Service.Handler.Commands.LoanRepayment
{
    public class CreateLoanRepaymentCommand : IRequest<LoanRepaymentDto>
    {
        public LoanRepaymentDto LoanRepayment { get; set; }

        public CreateLoanRepaymentCommand(LoanRepaymentDto loanRepayment)
        {
            LoanRepayment = loanRepayment;
        }
    }

    public class CreateLoanRepaymentCommandHandler : IRequestHandler<CreateLoanRepaymentCommand, LoanRepaymentDto>
    {
        private readonly AppDbContext _context;

        public CreateLoanRepaymentCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LoanRepaymentDto> Handle(CreateLoanRepaymentCommand request, CancellationToken cancellationToken)
        {
            var entity = new LoanRepaymentEntity
            {
                LoanRequestID = request.LoanRepayment.LoanRequestId,
                PaymentDate = request.LoanRepayment.PaymentDate,
                Amount = request.LoanRepayment.Amount,
                Balance = request.LoanRepayment.Balance,
                ReceiptNumber = request.LoanRepayment.ReceiptNumber,
                CreatedBy = request.LoanRepayment.CreatedBy,
                PaymentMethod = request.LoanRepayment.PaymentMethod,
                PaymentGatewayRef = request.LoanRepayment.PaymentGatewayRef,
                CreatedAt = DateTime.UtcNow,
                RevSeq = 1,
                Status = request.LoanRepayment.Status,
                RandId = request.LoanRepayment.RandId
            };

            _context.LoanRepayments.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            request.LoanRepayment.RowId = entity.RowId;
            return request.LoanRepayment;
        }
    }

    public class UpdateLoanRepaymentCommand : IRequest<LoanRepaymentDto>
    {
        public LoanRepaymentDto LoanRepayment { get; set; }

        public UpdateLoanRepaymentCommand(LoanRepaymentDto loanRepayment)
        {
            LoanRepayment = loanRepayment;
        }
    }

    public class UpdateLoanRepaymentCommandHandler : IRequestHandler<UpdateLoanRepaymentCommand, LoanRepaymentDto>
    {
        private readonly AppDbContext _context;

        public UpdateLoanRepaymentCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LoanRepaymentDto> Handle(UpdateLoanRepaymentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.LoanRepayments
                .FirstOrDefaultAsync(lr => lr.RowId == request.LoanRepayment.RowId, cancellationToken);

            if (entity == null)
            {
                throw new KeyNotFoundException($"LoanRepayment with RowId {request.LoanRepayment.RowId} not found.");
            }

            entity.LoanRequestID = request.LoanRepayment.LoanRequestId;
            entity.PaymentDate = request.LoanRepayment.PaymentDate;
            entity.Amount = request.LoanRepayment.Amount;
            entity.Balance = request.LoanRepayment.Balance;
            entity.ReceiptNumber = request.LoanRepayment.ReceiptNumber;
            entity.CreatedBy = request.LoanRepayment.CreatedBy;
            entity.PaymentMethod = request.LoanRepayment.PaymentMethod;
            entity.PaymentGatewayRef = request.LoanRepayment.PaymentGatewayRef;
            entity.Status = request.LoanRepayment.Status;
            entity.RandId = request.LoanRepayment.RandId;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.RevSeq++;

            await _context.SaveChangesAsync(cancellationToken);

            return request.LoanRepayment;
        }
    }

    public class DeleteLoanRepaymentCommand : IRequest<bool>
    {
        public int RowId { get; set; }

        public DeleteLoanRepaymentCommand(int rowId)
        {
            RowId = rowId;
        }
    }

    public class DeleteLoanRepaymentCommandHandler : IRequestHandler<DeleteLoanRepaymentCommand, bool>
    {
        private readonly AppDbContext _context;

        public DeleteLoanRepaymentCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteLoanRepaymentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.LoanRepayments
                .FirstOrDefaultAsync(lr => lr.RowId == request.RowId, cancellationToken);

            if (entity == null)
            {
                return false;
            }

            _context.LoanRepayments.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}