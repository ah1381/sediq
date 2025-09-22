using FluentValidation;
using LoanManagement.Service.Handler.Commands.FundExecutive;

namespace LoanManagement.WebApi.Validations
{
    public class CreateFundExecutiveValidator : AbstractValidator<CreateFundExecutiveCommand>
    {
        public CreateFundExecutiveValidator()
        {
            RuleFor(x => x.FundExecutive)
                .NotNull()
                .WithMessage("FundExecutive data is required.");

            RuleFor(x => x.FundExecutive.FundId)
                .GreaterThan(0)
                .WithMessage("FundId must be greater than 0.");

            RuleFor(x => x.FundExecutive.PersonnelId)
                .GreaterThan(0)
                .WithMessage("PersonnelId must be greater than 0.");

            RuleFor(x => x.FundExecutive.Role)
                .NotEmpty()
                .WithMessage("Role is required.")
                .MaximumLength(50)
                .WithMessage("Role must not exceed 50 characters.");

            RuleFor(x => x.FundExecutive.CreatedBy)
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundExecutive.CreatedBy));

            RuleFor(x => x.FundExecutive.OrderNumber)
                .MaximumLength(50)
                .WithMessage("OrderNumber must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundExecutive.OrderNumber));
        }
    }

    public class UpdateFundExecutiveValidator : AbstractValidator<UpdateFundExecutiveCommand>
    {
        public UpdateFundExecutiveValidator()
        {
            RuleFor(x => x.FundExecutive)
                .NotNull()
                .WithMessage("FundExecutive data is required.");

            RuleFor(x => x.FundExecutive.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");

            RuleFor(x => x.FundExecutive.FundId)
                .GreaterThan(0)
                .WithMessage("FundId must be greater than 0.");

            RuleFor(x => x.FundExecutive.PersonnelId)
                .GreaterThan(0)
                .WithMessage("PersonnelId must be greater than 0.");

            RuleFor(x => x.FundExecutive.Role)
                .NotEmpty()
                .WithMessage("Role is required.")
                .MaximumLength(50)
                .WithMessage("Role must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundExecutive.Role));

            RuleFor(x => x.FundExecutive.CreatedBy)
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundExecutive.CreatedBy));

            RuleFor(x => x.FundExecutive.OrderNumber)
                .MaximumLength(50)
                .WithMessage("OrderNumber must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundExecutive.OrderNumber));
        }
    }

    public class DeleteFundExecutiveValidator : AbstractValidator<DeleteFundExecutiveCommand>
    {
        public DeleteFundExecutiveValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");
        }
    }
}
