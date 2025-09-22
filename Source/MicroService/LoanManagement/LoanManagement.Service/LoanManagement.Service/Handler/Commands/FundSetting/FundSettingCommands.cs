using LoanManagement.Domain.Data;
using LoanManagement.Domain.Entities;
using LoanManagement.Service.Models.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LoanManagement.Service.Handler.Commands.FundSetting
{
    public class CreateFundSettingCommand : IRequest<FundSettingDto>
    {
        public FundSettingDto FundSetting { get; set; }

        public CreateFundSettingCommand(FundSettingDto fundSetting)
        {
            FundSetting = fundSetting;
        }
    }

    public class CreateFundSettingCommandHandler : IRequestHandler<CreateFundSettingCommand, FundSettingDto>
    {
        private readonly AppDbContext _context;

        public CreateFundSettingCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FundSettingDto> Handle(CreateFundSettingCommand request, CancellationToken cancellationToken)
        {
            var entity = new FundSettingEntity
            {
                FundID = request.FundSetting.FundId,
                MaxLoanAmount = request.FundSetting.MaxLoanAmount,
                LoanMultiplier = request.FundSetting.LoanMultiplier,
                CommissionPercent = request.FundSetting.CommissionPercent,
                MinShareAmount = request.FundSetting.MinShareAmount,
                MinMembershipMonths = request.FundSetting.MinMembershipMonths,
                SingleLoanPerMember = request.FundSetting.SingleLoanPerMember,
                MultiPaymentAllowed = request.FundSetting.MultiPaymentAllowed,
                MaxInstallmentChange = request.FundSetting.MaxInstallmentChange,
                RepaymentMonths = request.FundSetting.RepaymentMonths,
                InsurancePercent = request.FundSetting.InsurancePercent,
                CreatedAt = DateTime.UtcNow,
                RevSeq = 1,
                Status = request.FundSetting.Status,
                RandId = request.FundSetting.RandId
            };

            _context.FundSettings.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            request.FundSetting.RowId = entity.RowId;
            return request.FundSetting;
        }
    }

    public class UpdateFundSettingCommand : IRequest<FundSettingDto>
    {
        public FundSettingDto FundSetting { get; set; }

        public UpdateFundSettingCommand(FundSettingDto fundSetting)
        {
            FundSetting = fundSetting;
        }
    }

    public class UpdateFundSettingCommandHandler : IRequestHandler<UpdateFundSettingCommand, FundSettingDto>
    {
        private readonly AppDbContext _context;

        public UpdateFundSettingCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FundSettingDto> Handle(UpdateFundSettingCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FundSettings
                .FirstOrDefaultAsync(fs => fs.RowId == request.FundSetting.RowId, cancellationToken);

            if (entity == null)
            {
                throw new KeyNotFoundException($"FundSetting with RowId {request.FundSetting.RowId} not found.");
            }

            entity.FundID = request.FundSetting.FundId;
            entity.MaxLoanAmount = request.FundSetting.MaxLoanAmount;
            entity.LoanMultiplier = request.FundSetting.LoanMultiplier;
            entity.CommissionPercent = request.FundSetting.CommissionPercent;
            entity.MinShareAmount = request.FundSetting.MinShareAmount;
            entity.MinMembershipMonths = request.FundSetting.MinMembershipMonths;
            entity.SingleLoanPerMember = request.FundSetting.SingleLoanPerMember;
            entity.MultiPaymentAllowed = request.FundSetting.MultiPaymentAllowed;
            entity.MaxInstallmentChange = request.FundSetting.MaxInstallmentChange;
            entity.RepaymentMonths = request.FundSetting.RepaymentMonths;
            entity.InsurancePercent = request.FundSetting.InsurancePercent;
            entity.Status = request.FundSetting.Status;
            entity.RandId = request.FundSetting.RandId;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.RevSeq++;

            await _context.SaveChangesAsync(cancellationToken);

            return request.FundSetting;
        }
    }

    public class DeleteFundSettingCommand : IRequest<bool>
    {
        public int RowId { get; set; }

        public DeleteFundSettingCommand(int rowId)
        {
            RowId = rowId;
        }
    }

    public class DeleteFundSettingCommandHandler : IRequestHandler<DeleteFundSettingCommand, bool>
    {
        private readonly AppDbContext _context;

        public DeleteFundSettingCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteFundSettingCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FundSettings
                .FirstOrDefaultAsync(fs => fs.RowId == request.RowId, cancellationToken);

            if (entity == null)
            {
                return false;
            }

            _context.FundSettings.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}