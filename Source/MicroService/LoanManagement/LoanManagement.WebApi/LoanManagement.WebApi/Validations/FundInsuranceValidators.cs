using FluentValidation;
using LoanManagement.Service.Handler.Commands.FundInsurance;

namespace LoanManagement.WebApi.Validations
{
    public class CreateFundInsuranceValidator : AbstractValidator<CreateFundInsuranceCommand>
    {
        public CreateFundInsuranceValidator()
        {
            RuleFor(x => x.FundInsurance)
                .NotNull()
                .WithMessage("FundInsurance data is required.");

            RuleFor(x => x.FundInsurance.FundId)
                .GreaterThan(0)
                .WithMessage("FundId must be greater than 0.");

            RuleFor(x => x.FundInsurance.InsuranceAmount)
                .GreaterThan(0)
                .WithMessage("InsuranceAmount must be greater than 0.");

            RuleFor(x => x.FundInsurance.Provider)
                .NotEmpty()
                .WithMessage("Provider is required.")
                .MaximumLength(100)
                .WithMessage("Provider must not exceed 100 characters.");

            RuleFor(x => x.FundInsurance.CreatedBy)
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundInsurance.CreatedBy));
        }
    }

    public class UpdateFundInsuranceValidator : AbstractValidator<UpdateFundInsuranceCommand>
    {
        public UpdateFundInsuranceValidator()
        {
            RuleFor(x => x.FundInsurance)
                .NotNull()
                .WithMessage("FundInsurance data is required.");

            RuleFor(x => x.FundInsurance.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");

            RuleFor(x => x.FundInsurance.FundId)
                .GreaterThan(0)
                .WithMessage("FundId must be greater than 0.");

            RuleFor(x => x.FundInsurance.InsuranceAmount)
                .GreaterThan(0)
                .WithMessage("InsuranceAmount must be greater than 0.");

            RuleFor(x => x.FundInsurance.Provider)
                .NotEmpty()
                .WithMessage("Provider is required.")
                .MaximumLength(100)
                .WithMessage("Provider must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundInsurance.Provider));

            RuleFor(x => x.FundInsurance.CreatedBy)
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundInsurance.CreatedBy));
        }
    }

    public class DeleteFundInsuranceValidator : AbstractValidator<DeleteFundInsuranceCommand>
    {
        public DeleteFundInsuranceValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");
        }
    }
}
