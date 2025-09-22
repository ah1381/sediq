using LoanManagement.Domain.Data;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LoanManagement.Service.Handler.Commands.FundMember
{
    public class CreateFundMemberCommand : IRequest<FundMemberDto>
    {
        public FundMemberDto FundMember { get; set; }

        public CreateFundMemberCommand(FundMemberDto fundMember)
        {
            FundMember = fundMember;
        }
    }

    public class CreateFundMemberCommandHandler : IRequestHandler<CreateFundMemberCommand, FundMemberDto>
    {
        private readonly AppDbContext _context;

        public CreateFundMemberCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FundMemberDto> Handle(CreateFundMemberCommand request, CancellationToken cancellationToken)
        {
            var entity = new FundMemberEntity
            {
                PersonnelID = request.FundMember.PersonnelId,
                FundID = request.FundMember.FundId,
                MembershipType = request.FundMember.MembershipType,
                MembershipNumber = request.FundMember.MembershipNumber,
                StartDate = request.FundMember.StartDate,
                EndDate = request.FundMember.EndDate,
                Shares = request.FundMember.Shares,
                ShareDeductionCodes = request.FundMember.ShareDeductionCodes,
                BankAccount = request.FundMember.BankAccount,
                EmploymentType = request.FundMember.EmploymentType,
                Position = request.FundMember.Position,
                Email = request.FundMember.Email,
                Phone = request.FundMember.Phone,
                StatusDesc = request.FundMember.StatusDesc,
                CreatedBy = request.FundMember.CreatedBy,
                TotalSalary = request.FundMember.TotalSalary,
                NetPayment = request.FundMember.NetPayment,
                Province = request.FundMember.Province,
                WaitingMonths = request.FundMember.WaitingMonths,
                CreatedAt = DateTime.UtcNow,
                RevSeq = 1,
                Status = request.FundMember.Status,
                RandId = request.FundMember.RandId
            };

            _context.FundMembers.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            request.FundMember.RowId = entity.RowId;
            return request.FundMember;
        }
    }

    public class UpdateFundMemberCommand : IRequest<FundMemberDto>
    {
        public FundMemberDto FundMember { get; set; }

        public UpdateFundMemberCommand(FundMemberDto fundMember)
        {
            FundMember = fundMember;
        }
    }

    public class UpdateFundMemberCommandHandler : IRequestHandler<UpdateFundMemberCommand, FundMemberDto>
    {
        private readonly AppDbContext _context;

        public UpdateFundMemberCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FundMemberDto> Handle(UpdateFundMemberCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FundMembers
                .FirstOrDefaultAsync(fm => fm.RowId == request.FundMember.RowId, cancellationToken);

            if (entity == null)
            {
                throw new KeyNotFoundException($"FundMember with RowId {request.FundMember.RowId} not found.");
            }

            entity.PersonnelID = request.FundMember.PersonnelId;
            entity.FundID = request.FundMember.FundId;
            entity.MembershipType = request.FundMember.MembershipType;
            entity.MembershipNumber = request.FundMember.MembershipNumber;
            entity.StartDate = request.FundMember.StartDate;
            entity.EndDate = request.FundMember.EndDate;
            entity.Shares = request.FundMember.Shares;
            entity.ShareDeductionCodes = request.FundMember.ShareDeductionCodes;
            entity.BankAccount = request.FundMember.BankAccount;
            entity.EmploymentType = request.FundMember.EmploymentType;
            entity.Position = request.FundMember.Position;
            entity.Email = request.FundMember.Email;
            entity.Phone = request.FundMember.Phone;
            entity.StatusDesc = request.FundMember.StatusDesc;
            entity.CreatedBy = request.FundMember.CreatedBy;
            entity.TotalSalary = request.FundMember.TotalSalary;
            entity.NetPayment = request.FundMember.NetPayment;
            entity.Province = request.FundMember.Province;
            entity.WaitingMonths = request.FundMember.WaitingMonths;
            entity.Status = request.FundMember.Status;
            entity.RandId = request.FundMember.RandId;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.RevSeq++;

            await _context.SaveChangesAsync(cancellationToken);

            return request.FundMember;
        }
    }

    public class DeleteFundMemberCommand : IRequest<bool>
    {
        public int RowId { get; set; }

        public DeleteFundMemberCommand(int rowId)
        {
            RowId = rowId;
        }
    }

    public class DeleteFundMemberCommandHandler : IRequestHandler<DeleteFundMemberCommand, bool>
    {
        private readonly AppDbContext _context;

        public DeleteFundMemberCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteFundMemberCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FundMembers
                .FirstOrDefaultAsync(fm => fm.RowId == request.RowId, cancellationToken);

            if (entity == null)
            {
                return false;
            }

            _context.FundMembers.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}