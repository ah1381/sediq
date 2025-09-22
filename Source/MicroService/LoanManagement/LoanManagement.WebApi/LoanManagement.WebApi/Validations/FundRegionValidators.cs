using FluentValidation;
using LoanManagement.Service.Handler.Commands.FundRegion;

namespace LoanManagement.WebApi.Validations
{
    public class CreateFundRegionValidator : AbstractValidator<CreateFundRegionCommand>
    {
        public CreateFundRegionValidator()
        {
            RuleFor(x => x.FundRegion)
                .NotNull()
                .WithMessage("FundRegion data is required.");

            RuleFor(x => x.FundRegion.FundId)
                .GreaterThan(0)
                .WithMessage("FundId must be greater than 0.");

            RuleFor(x => x.FundRegion.RegionName)
                .NotEmpty()
                .WithMessage("RegionName is required.")
                .MaximumLength(100)
                .WithMessage("RegionName must not exceed 100 characters.");

            RuleFor(x => x.FundRegion.ActiveMembers)
                .GreaterThanOrEqualTo(0)
                .WithMessage("ActiveMembers must be greater than or equal to 0.");

            RuleFor(x => x.FundRegion.RetiredMembers)
                .GreaterThanOrEqualTo(0)
                .WithMessage("RetiredMembers must be greater than or equal to 0.");

            RuleFor(x => x.FundRegion.BankName)
                .MaximumLength(100)
                .WithMessage("BankName must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundRegion.BankName));

            RuleFor(x => x.FundRegion.BranchCode)
                .MaximumLength(50)
                .WithMessage("BranchCode must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundRegion.BranchCode));

            RuleFor(x => x.FundRegion.OrgCode)
                .MaximumLength(50)
                .WithMessage("OrgCode must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundRegion.OrgCode));

            RuleFor(x => x.FundRegion.InstallmentDeductionCodes)
                .NotNull()
                .WithMessage("InstallmentDeductionCodes is required.");

            RuleFor(x => x.FundRegion.ShareDeductionCodes)
                .NotNull()
                .WithMessage("ShareDeductionCodes is required.");
        }
    }

    public class UpdateFundRegionValidator : AbstractValidator<UpdateFundRegionCommand>
    {
        public UpdateFundRegionValidator()
        {
            RuleFor(x => x.FundRegion)
                .NotNull()
                .WithMessage("FundRegion data is required.");

            RuleFor(x => x.FundRegion.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");

            RuleFor(x => x.FundRegion.FundId)
                .GreaterThan(0)
                .WithMessage("FundId must be greater than 0.");

            RuleFor(x => x.FundRegion.RegionName)
                .NotEmpty()
                .WithMessage("RegionName is required.")
                .MaximumLength(100)
                .WithMessage("RegionName must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundRegion.RegionName));

            RuleFor(x => x.FundRegion.ActiveMembers)
                .GreaterThanOrEqualTo(0)
                .WithMessage("ActiveMembers must be greater than or equal to 0.");

            RuleFor(x => x.FundRegion.RetiredMembers)
                .GreaterThanOrEqualTo(0)
                .WithMessage("RetiredMembers must be greater than or equal to 0.");

            RuleFor(x => x.FundRegion.BankName)
                .MaximumLength(100)
                .WithMessage("BankName must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundRegion.BankName));

            RuleFor(x => x.FundRegion.BranchCode)
                .MaximumLength(50)
                .WithMessage("BranchCode must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundRegion.BranchCode));

            RuleFor(x => x.FundRegion.OrgCode)
                .MaximumLength(50)
                .WithMessage("OrgCode must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundRegion.OrgCode));

            RuleFor(x => x.FundRegion.InstallmentDeductionCodes)
                .NotNull()
                .WithMessage("InstallmentDeductionCodes is required.")
                .When(x => x.FundRegion.InstallmentDeductionCodes != null);

            RuleFor(x => x.FundRegion.ShareDeductionCodes)
                .NotNull()
                .WithMessage("ShareDeductionCodes is required.")
                .When(x => x.FundRegion.ShareDeductionCodes != null);
        }
    }

    public class DeleteFundRegionValidator : AbstractValidator<DeleteFundRegionCommand>
    {
        public DeleteFundRegionValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");
        }
    }
}