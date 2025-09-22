using FluentValidation;
using LoanManagement.Service.Handler.Commands.FundSetting;

namespace LoanManagement.WebApi.Validations
{
    public class CreateFundSettingValidator : AbstractValidator<CreateFundSettingCommand>
    {
        public CreateFundSettingValidator()
        {
            RuleFor(x => x.FundSetting)
                .NotNull()
                .WithMessage("FundSetting data is required.");

            RuleFor(x => x.FundSetting.FundId)
                .GreaterThan(0)
                .WithMessage("FundId must be greater than 0.");

            RuleFor(x => x.FundSetting.MaxLoanAmount)
                .GreaterThan(0)
                .WithMessage("MaxLoanAmount must be greater than 0.");

            RuleFor(x => x.FundSetting.LoanMultiplier)
                .GreaterThan(0)
                .WithMessage("LoanMultiplier must be greater than 0.");

            RuleFor(x => x.FundSetting.CommissionPercent)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100)
                .WithMessage("CommissionPercent must be between 0 and 100.");

            RuleFor(x => x.FundSetting.MinShareAmount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("MinShareAmount must be greater than or equal to 0.");

            RuleFor(x => x.FundSetting.MinMembershipMonths)
                .GreaterThanOrEqualTo(0)
                .WithMessage("MinMembershipMonths must be greater than or equal to 0.");

            RuleFor(x => x.FundSetting.RepaymentMonths)
                .GreaterThan(0)
                .WithMessage("RepaymentMonths must be greater than 0.");

            RuleFor(x => x.FundSetting.InsurancePercent)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100)
                .WithMessage("InsurancePercent must be between 0 and 100.");
        }
    }

    public class UpdateFundSettingValidator : AbstractValidator<UpdateFundSettingCommand>
    {
        public UpdateFundSettingValidator()
        {
            RuleFor(x => x.FundSetting)
                .NotNull()
                .WithMessage("FundSetting data is required.");

            RuleFor(x => x.FundSetting.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");

            RuleFor(x => x.FundSetting.FundId)
                .GreaterThan(0)
                .WithMessage("FundId must be greater than 0.");

            RuleFor(x => x.FundSetting.MaxLoanAmount)
                .GreaterThan(0)
                .WithMessage("MaxLoanAmount must be greater than 0.");

            RuleFor(x => x.FundSetting.LoanMultiplier)
                .GreaterThan(0)
                .WithMessage("LoanMultiplier must be greater than 0.");

            RuleFor(x => x.FundSetting.CommissionPercent)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100)
                .WithMessage("CommissionPercent must be between 0 and 100.");

            RuleFor(x => x.FundSetting.MinShareAmount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("MinShareAmount must be greater than or equal to 0.");

            RuleFor(x => x.FundSetting.MinMembershipMonths)
                .GreaterThanOrEqualTo(0)
                .WithMessage("MinMembershipMonths must be greater than or equal to 0.");

            RuleFor(x => x.FundSetting.RepaymentMonths)
                .GreaterThan(0)
                .WithMessage("RepaymentMonths must be greater than 0.");

            RuleFor(x => x.FundSetting.InsurancePercent)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100)
                .WithMessage("InsurancePercent must be between 0 and 100.");
        }
    }

    public class DeleteFundSettingValidator : AbstractValidator<DeleteFundSettingCommand>
    {
        public DeleteFundSettingValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");
        }
    }
}
