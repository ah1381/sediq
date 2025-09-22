using FluentValidation;
using LoanManagement.Service.Handler.Commands.Fund;

namespace LoanManagement.WebApi.Validations
{
    public class CreateFundValidator : AbstractValidator<CreateFundCommand>
    {
        public CreateFundValidator()
        {
            RuleFor(x => x.Fund)
                .NotNull()
                .WithMessage("Fund data is required.");

            RuleFor(x => x.Fund.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(200)
                .WithMessage("Name must not exceed 200 characters.");

            RuleFor(x => x.Fund.FundType)
                .NotEmpty()
                .WithMessage("FundType is required.")
                .MaximumLength(50)
                .WithMessage("FundType must not exceed 50 characters.");

            RuleFor(x => x.Fund.CalculationType)
                .NotEmpty()
                .WithMessage("CalculationType is required.")
                .MaximumLength(50)
                .WithMessage("CalculationType must not exceed 50 characters.");

            RuleFor(x => x.Fund.Region)
                .NotEmpty()
                .WithMessage("Region is required.")
                .MaximumLength(100)
                .WithMessage("Region must not exceed 100 characters.");

            RuleFor(x => x.Fund.Province)
                .NotEmpty()
                .WithMessage("Province is required.")
                .MaximumLength(100)
                .WithMessage("Province must not exceed 100 characters.");

            RuleFor(x => x.Fund.Level)
                .NotEmpty()
                .WithMessage("Level is required.")
                .MaximumLength(50)
                .WithMessage("Level must not exceed 50 characters.");

            RuleFor(x => x.Fund.CreatedBy)
                .NotEmpty()
                .WithMessage("CreatedBy is required.")
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters.");
        }
    }

    public class UpdateFundValidator : AbstractValidator<UpdateFundCommand>
    {
        public UpdateFundValidator()
        {
            RuleFor(x => x.Fund)
                .NotNull()
                .WithMessage("Fund data is required.");

            RuleFor(x => x.Fund.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");

            RuleFor(x => x.Fund.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(200)
                .WithMessage("Name must not exceed 200 characters.")
                .When(x => !string.IsNullOrEmpty(x.Fund.Name));

            RuleFor(x => x.Fund.FundType)
                .NotEmpty()
                .WithMessage("FundType is required.")
                .MaximumLength(50)
                .WithMessage("FundType must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.Fund.FundType));

            RuleFor(x => x.Fund.CalculationType)
                .NotEmpty()
                .WithMessage("CalculationType is required.")
                .MaximumLength(50)
                .WithMessage("CalculationType must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.Fund.CalculationType));

            RuleFor(x => x.Fund.Region)
                .NotEmpty()
                .WithMessage("Region is required.")
                .MaximumLength(100)
                .WithMessage("Region must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Fund.Region));

            RuleFor(x => x.Fund.Province)
                .NotEmpty()
                .WithMessage("Province is required.")
                .MaximumLength(100)
                .WithMessage("Province must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Fund.Province));

            RuleFor(x => x.Fund.Level)
                .NotEmpty()
                .WithMessage("Level is required.")
                .MaximumLength(50)
                .WithMessage("Level must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.Fund.Level));

            RuleFor(x => x.Fund.CreatedBy)
                .NotEmpty()
                .WithMessage("CreatedBy is required.")
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Fund.CreatedBy));
        }
    }

    public class DeleteFundValidator : AbstractValidator<DeleteFundCommand>
    {
        public DeleteFundValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");
        }
    }
}