using FluentValidation;
using LoanManagement.Service.Handler.Commands.LoanType;

namespace LoanManagement.WebApi.Validations
{
    public class CreateLoanTypeValidator : AbstractValidator<CreateLoanTypeCommand>
    {
        public CreateLoanTypeValidator()
        {
            RuleFor(x => x.LoanType)
                .NotNull()
                .WithMessage("LoanType data is required.");

            RuleFor(x => x.LoanType.FundId)
                .GreaterThan(0)
                .WithMessage("FundId must be greater than 0.");

            RuleFor(x => x.LoanType.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(100)
                .WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.LoanType.MaxAmount)
                .GreaterThan(0)
                .WithMessage("MaxAmount must be greater than 0.");

            RuleFor(x => x.LoanType.MinInstallments)
                .GreaterThan(0)
                .WithMessage("MinInstallments must be greater than 0.");

            RuleFor(x => x.LoanType.MaxInstallments)
                .GreaterThan(0)
                .WithMessage("MaxInstallments must be greater than 0.");

            RuleFor(x => x.LoanType.CreatedBy)
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanType.CreatedBy));
        }
    }

    public class UpdateLoanTypeValidator : AbstractValidator<UpdateLoanTypeCommand>
    {
        public UpdateLoanTypeValidator()
        {
            RuleFor(x => x.LoanType)
                .NotNull()
                .WithMessage("LoanType data is required.");

            RuleFor(x => x.LoanType.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");

            RuleFor(x => x.LoanType.FundId)
                .GreaterThan(0)
                .WithMessage("FundId must be greater than 0.");

            RuleFor(x => x.LoanType.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(100)
                .WithMessage("Name must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanType.Name));

            RuleFor(x => x.LoanType.MaxAmount)
                .GreaterThan(0)
                .WithMessage("MaxAmount must be greater than 0.");

            RuleFor(x => x.LoanType.MinInstallments)
                .GreaterThan(0)
                .WithMessage("MinInstallments must be greater than 0.");

            RuleFor(x => x.LoanType.MaxInstallments)
                .GreaterThan(0)
                .WithMessage("MaxInstallments must be greater than 0.");

            RuleFor(x => x.LoanType.CreatedBy)
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanType.CreatedBy));
        }
    }

    public class DeleteLoanTypeValidator : AbstractValidator<DeleteLoanTypeCommand>
    {
        public DeleteLoanTypeValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");
        }
    }
}