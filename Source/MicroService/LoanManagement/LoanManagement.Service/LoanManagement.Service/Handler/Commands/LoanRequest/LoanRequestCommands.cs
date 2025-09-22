using LoanManagement.Domain.Data;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LoanManagement.Service.Handler.Commands.LoanRequest
{
    public class CreateLoanRequestCommand : IRequest<LoanRequestDto>
    {
        public LoanRequestDto LoanRequest { get; set; }

        public CreateLoanRequestCommand(LoanRequestDto loanRequest)
        {
            LoanRequest = loanRequest;
        }
    }

    public class CreateLoanRequestCommandHandler : IRequestHandler<CreateLoanRequestCommand, LoanRequestDto>
    {
        private readonly AppDbContext _context;

        public CreateLoanRequestCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LoanRequestDto> Handle(CreateLoanRequestCommand request, CancellationToken cancellationToken)
        {
            var entity = new LoanRequestEntity
            {
                PersonnelID = request.LoanRequest.PersonnelId,
                LoanTypeID = request.LoanRequest.LoanTypeId,
                FundID = request.LoanRequest.FundId,
                RequestDate = request.LoanRequest.RequestDate,
                AmountRequested = request.LoanRequest.AmountRequested,
                MaxLoanAmount = request.LoanRequest.MaxLoanAmount,
                WaitingMonths = request.LoanRequest.WaitingMonths,
                StatusDesc = request.LoanRequest.StatusDesc,
                Installments = request.LoanRequest.Installments,
                MonthlyInstallment = request.LoanRequest.MonthlyInstallment,
                BankAccount = request.LoanRequest.BankAccount,
                Address = request.LoanRequest.Address,
                Phone = request.LoanRequest.Phone,
                Mobile = request.LoanRequest.Mobile,
                RequiresGuarantor = request.LoanRequest.RequiresGuarantor,
                CreatedBy = request.LoanRequest.CreatedBy,
                Province = request.LoanRequest.Province,
                Region = request.LoanRequest.Region,
                EmploymentType = request.LoanRequest.EmploymentType,
                Department = request.LoanRequest.Department,
                Online = request.LoanRequest.Online,
                CreatedAt = DateTime.UtcNow,
                RevSeq = 1,
                Status = request.LoanRequest.Status,
                RandId = request.LoanRequest.RandId
            };

            _context.LoanRequests.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            request.LoanRequest.RowId = entity.RowId;
            return request.LoanRequest;
        }
    }

    public class UpdateLoanRequestCommand : IRequest<LoanRequestDto>
    {
        public LoanRequestDto LoanRequest { get; set; }

        public UpdateLoanRequestCommand(LoanRequestDto loanRequest)
        {
            LoanRequest = loanRequest;
        }
    }

    public class UpdateLoanRequestCommandHandler : IRequestHandler<UpdateLoanRequestCommand, LoanRequestDto>
    {
        private readonly AppDbContext _context;

        public UpdateLoanRequestCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LoanRequestDto> Handle(UpdateLoanRequestCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.LoanRequests
                .FirstOrDefaultAsync(lr => lr.RowId == request.LoanRequest.RowId, cancellationToken);

            if (entity == null)
            {
                throw new KeyNotFoundException($"LoanRequest with RowId {request.LoanRequest.RowId} not found.");
            }

            entity.PersonnelID = request.LoanRequest.PersonnelId;
            entity.LoanTypeID = request.LoanRequest.LoanTypeId;
            entity.FundID = request.LoanRequest.FundId;
            entity.RequestDate = request.LoanRequest.RequestDate;
            entity.AmountRequested = request.LoanRequest.AmountRequested;
            entity.MaxLoanAmount = request.LoanRequest.MaxLoanAmount;
            entity.WaitingMonths = request.LoanRequest.WaitingMonths;
            entity.StatusDesc = request.LoanRequest.StatusDesc;
            entity.Installments = request.LoanRequest.Installments;
            entity.MonthlyInstallment = request.LoanRequest.MonthlyInstallment;
            entity.BankAccount = request.LoanRequest.BankAccount;
            entity.Address = request.LoanRequest.Address;
            entity.Phone = request.LoanRequest.Phone;
            entity.Mobile = request.LoanRequest.Mobile;
            entity.RequiresGuarantor = request.LoanRequest.RequiresGuarantor;
            entity.CreatedBy = request.LoanRequest.CreatedBy;
            entity.Province = request.LoanRequest.Province;
            entity.Region = request.LoanRequest.Region;
            entity.EmploymentType = request.LoanRequest.EmploymentType;
            entity.Department = request.LoanRequest.Department;
            entity.Online = request.LoanRequest.Online;
            entity.Status = request.LoanRequest.Status;
            entity.RandId = request.LoanRequest.RandId;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.RevSeq++;

            await _context.SaveChangesAsync(cancellationToken);

            return request.LoanRequest;
        }
    }

    public class DeleteLoanRequestCommand : IRequest<bool>
    {
        public int RowId { get; set; }

        public DeleteLoanRequestCommand(int rowId)
        {
            RowId = rowId;
        }
    }

    public class DeleteLoanRequestCommandHandler : IRequestHandler<DeleteLoanRequestCommand, bool>
    {
        private readonly AppDbContext _context;

        public DeleteLoanRequestCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteLoanRequestCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.LoanRequests
                .FirstOrDefaultAsync(lr => lr.RowId == request.RowId, cancellationToken);

            if (entity == null)
            {
                return false;
            }

            _context.LoanRequests.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
