using LoanManagement.Domain.Data;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LoanManagement.Service.Handler.Commands.LoanCertificate
{
    public class CreateLoanCertificateCommand : IRequest<LoanCertificateDto>
    {
        public LoanCertificateDto LoanCertificate { get; set; }

        public CreateLoanCertificateCommand(LoanCertificateDto loanCertificate)
        {
            LoanCertificate = loanCertificate;
        }
    }

    public class CreateLoanCertificateCommandHandler : IRequestHandler<CreateLoanCertificateCommand, LoanCertificateDto>
    {
        private readonly AppDbContext _context;

        public CreateLoanCertificateCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LoanCertificateDto> Handle(CreateLoanCertificateCommand request, CancellationToken cancellationToken)
        {
            var entity = new LoanCertificateEntity
            {
                LoanRequestID = request.LoanCertificate.LoanRequestId,
                CertificateNumber = request.LoanCertificate.CertificateNumber,
                IssueDate = request.LoanCertificate.IssueDate,
                Description = request.LoanCertificate.Description,
                CreatedAt = DateTime.UtcNow,
                RevSeq = 1,
                Status = request.LoanCertificate.Status,
                RandId = request.LoanCertificate.RandId
            };

            _context.LoanCertificates.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            request.LoanCertificate.RowId = entity.RowId;
            return request.LoanCertificate;
        }
    }

    public class UpdateLoanCertificateCommand : IRequest<LoanCertificateDto>
    {
        public LoanCertificateDto LoanCertificate { get; set; }

        public UpdateLoanCertificateCommand(LoanCertificateDto loanCertificate)
        {
            LoanCertificate = loanCertificate;
        }
    }

    public class UpdateLoanCertificateCommandHandler : IRequestHandler<UpdateLoanCertificateCommand, LoanCertificateDto>
    {
        private readonly AppDbContext _context;

        public UpdateLoanCertificateCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LoanCertificateDto> Handle(UpdateLoanCertificateCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.LoanCertificates
                .FirstOrDefaultAsync(lc => lc.RowId == request.LoanCertificate.RowId, cancellationToken);

            if (entity == null)
            {
                throw new KeyNotFoundException($"LoanCertificate with RowId {request.LoanCertificate.RowId} not found.");
            }

            entity.LoanRequestID = request.LoanCertificate.LoanRequestId;
            entity.CertificateNumber = request.LoanCertificate.CertificateNumber;
            entity.IssueDate = request.LoanCertificate.IssueDate;
            entity.Description = request.LoanCertificate.Description;
            entity.Status = request.LoanCertificate.Status;
            entity.RandId = request.LoanCertificate.RandId;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.RevSeq++;

            await _context.SaveChangesAsync(cancellationToken);

            return request.LoanCertificate;
        }
    }

    public class DeleteLoanCertificateCommand : IRequest<bool>
    {
        public int RowId { get; set; }

        public DeleteLoanCertificateCommand(int rowId)
        {
            RowId = rowId;
        }
    }

    public class DeleteLoanCertificateCommandHandler : IRequestHandler<DeleteLoanCertificateCommand, bool>
    {
        private readonly AppDbContext _context;

        public DeleteLoanCertificateCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteLoanCertificateCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.LoanCertificates
                .FirstOrDefaultAsync(lc => lc.RowId == request.RowId, cancellationToken);

            if (entity == null)
            {
                return false;
            }

            _context.LoanCertificates.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}