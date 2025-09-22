using LoanManagement.Domain.Data;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LoanManagement.Service.Handler.Commands.LoanGuarantor
{
    public class CreateLoanGuarantorCommand : IRequest<LoanGuarantorDto>
    {
        public LoanGuarantorDto LoanGuarantor { get; set; }

        public CreateLoanGuarantorCommand(LoanGuarantorDto loanGuarantor)
        {
            LoanGuarantor = loanGuarantor;
        }
    }

    public class CreateLoanGuarantorCommandHandler : IRequestHandler<CreateLoanGuarantorCommand, LoanGuarantorDto>
    {
        private readonly AppDbContext _context;

        public CreateLoanGuarantorCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LoanGuarantorDto> Handle(CreateLoanGuarantorCommand request, CancellationToken cancellationToken)
        {
            var entity = new LoanGuarantorEntity
            {
                LoanRequestID = request.LoanGuarantor.LoanRequestId,
                PersonnelID = request.LoanGuarantor.PersonnelId,
                GuarantorCode = request.LoanGuarantor.GuarantorCode,
                GuaranteeDate = request.LoanGuarantor.GuaranteeDate,
                CreatedBy = request.LoanGuarantor.CreatedBy,
                CreatedAt = DateTime.UtcNow,
                RevSeq = 1,
                Status = request.LoanGuarantor.Status,
                RandId = request.LoanGuarantor.RandId
            };

            _context.LoanGuarantors.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            request.LoanGuarantor.RowId = entity.RowId;
            return request.LoanGuarantor;
        }
    }

    public class UpdateLoanGuarantorCommand : IRequest<LoanGuarantorDto>
    {
        public LoanGuarantorDto LoanGuarantor { get; set; }

        public UpdateLoanGuarantorCommand(LoanGuarantorDto loanGuarantor)
        {
            LoanGuarantor = loanGuarantor;
        }
    }

    public class UpdateLoanGuarantorCommandHandler : IRequestHandler<UpdateLoanGuarantorCommand, LoanGuarantorDto>
    {
        private readonly AppDbContext _context;

        public UpdateLoanGuarantorCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LoanGuarantorDto> Handle(UpdateLoanGuarantorCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.LoanGuarantors
                .FirstOrDefaultAsync(lg => lg.RowId == request.LoanGuarantor.RowId, cancellationToken);

            if (entity == null)
            {
                throw new KeyNotFoundException($"LoanGuarantor with RowId {request.LoanGuarantor.RowId} not found.");
            }

            entity.LoanRequestID = request.LoanGuarantor.LoanRequestId;
            entity.PersonnelID = request.LoanGuarantor.PersonnelId;
            entity.GuarantorCode = request.LoanGuarantor.GuarantorCode;
            entity.GuaranteeDate = request.LoanGuarantor.GuaranteeDate;
            entity.CreatedBy = request.LoanGuarantor.CreatedBy;
            entity.Status = request.LoanGuarantor.Status;
            entity.RandId = request.LoanGuarantor.RandId;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.RevSeq++;

            await _context.SaveChangesAsync(cancellationToken);

            return request.LoanGuarantor;
        }
    }

    public class DeleteLoanGuarantorCommand : IRequest<bool>
    {
        public int RowId { get; set; }

        public DeleteLoanGuarantorCommand(int rowId)
        {
            RowId = rowId;
        }
    }

    public class DeleteLoanGuarantorCommandHandler : IRequestHandler<DeleteLoanGuarantorCommand, bool>
    {
        private readonly AppDbContext _context;

        public DeleteLoanGuarantorCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteLoanGuarantorCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.LoanGuarantors
                .FirstOrDefaultAsync(lg => lg.RowId == request.RowId, cancellationToken);

            if (entity == null)
            {
                return false;
            }

            _context.LoanGuarantors.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
