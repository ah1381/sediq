using LoanManagement.Domain.Data;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LoanManagement.Service.Handler.Commands.LoanType
{
    public class CreateLoanTypeCommand : IRequest<LoanTypeDto>
    {
        public LoanTypeDto LoanType { get; set; }

        public CreateLoanTypeCommand(LoanTypeDto loanType)
        {
            LoanType = loanType;
        }
    }

    public class CreateLoanTypeCommandHandler : IRequestHandler<CreateLoanTypeCommand, LoanTypeDto>
    {
        private readonly AppDbContext _context;

        public CreateLoanTypeCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LoanTypeDto> Handle(CreateLoanTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = new LoanTypeEntity
            {
                FundID = request.LoanType.FundId,
                Name = request.LoanType.Name,
                Description = request.LoanType.Description,
                MaxAmount = request.LoanType.MaxAmount,
                MinInstallments = request.LoanType.MinInstallments,
                MaxInstallments = request.LoanType.MaxInstallments,
                StartDate = request.LoanType.StartDate,
                EndDate = request.LoanType.EndDate,
                CreatedBy = request.LoanType.CreatedBy,
                CreatedAt = DateTime.UtcNow,
                RevSeq = 1,
                Status = request.LoanType.Status,
                RandId = request.LoanType.RandId
            };

            _context.LoanTypes.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            request.LoanType.RowId = entity.RowId;
            return request.LoanType;
        }
    }

    public class UpdateLoanTypeCommand : IRequest<LoanTypeDto>
    {
        public LoanTypeDto LoanType { get; set; }

        public UpdateLoanTypeCommand(LoanTypeDto loanType)
        {
            LoanType = loanType;
        }
    }

    public class UpdateLoanTypeCommandHandler : IRequestHandler<UpdateLoanTypeCommand, LoanTypeDto>
    {
        private readonly AppDbContext _context;

        public UpdateLoanTypeCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LoanTypeDto> Handle(UpdateLoanTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.LoanTypes
                .FirstOrDefaultAsync(lt => lt.RowId == request.LoanType.RowId, cancellationToken);

            if (entity == null)
            {
                throw new KeyNotFoundException($"LoanType with RowId {request.LoanType.RowId} not found.");
            }

            entity.FundID = request.LoanType.FundId;
            entity.Name = request.LoanType.Name;
            entity.Description = request.LoanType.Description;
            entity.MaxAmount = request.LoanType.MaxAmount;
            entity.MinInstallments = request.LoanType.MinInstallments;
            entity.MaxInstallments = request.LoanType.MaxInstallments;
            entity.StartDate = request.LoanType.StartDate;
            entity.EndDate = request.LoanType.EndDate;
            entity.CreatedBy = request.LoanType.CreatedBy;
            entity.Status = request.LoanType.Status;
            entity.RandId = request.LoanType.RandId;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.RevSeq++;

            await _context.SaveChangesAsync(cancellationToken);

            return request.LoanType;
        }
    }

    public class DeleteLoanTypeCommand : IRequest<bool>
    {
        public int RowId { get; set; }

        public DeleteLoanTypeCommand(int rowId)
        {
            RowId = rowId;
        }
    }

    public class DeleteLoanTypeCommandHandler : IRequestHandler<DeleteLoanTypeCommand, bool>
    {
        private readonly AppDbContext _context;

        public DeleteLoanTypeCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteLoanTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.LoanTypes
                .FirstOrDefaultAsync(lt => lt.RowId == request.RowId, cancellationToken);

            if (entity == null)
            {
                return false;
            }

            _context.LoanTypes.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}