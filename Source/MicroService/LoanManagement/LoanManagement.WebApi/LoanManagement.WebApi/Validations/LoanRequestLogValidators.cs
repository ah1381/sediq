using FluentValidation;
using LoanManagement.Service.Handler.Commands.LoanRequestLog;

namespace LoanManagement.WebApi.Validations
{
    public class CreateLoanRequestLogValidator : AbstractValidator<CreateLoanRequestLogCommand>
    {
        public CreateLoanRequestLogValidator()
        {
            RuleFor(x => x.LoanRequestLog)
                .NotNull()
                .WithMessage("LoanRequestLog data is required.");

            RuleFor(x => x.LoanRequestLog.LoanRequestId)
                .GreaterThan(0)
                .WithMessage("LoanRequestId must be greater than 0.");

            RuleFor(x => x.LoanRequestLog.Action)
                .NotEmpty()
                .WithMessage("Action is required.")
                .MaximumLength(50)
                .WithMessage("Action must not exceed 50 characters.");

            RuleFor(x => x.LoanRequestLog.ActionBy)
                .MaximumLength(100)
                .WithMessage("ActionBy must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanRequestLog.ActionBy));
        }
    }

    public class UpdateLoanRequestLogValidator : AbstractValidator<UpdateLoanRequestLogCommand>
    {
        public UpdateLoanRequestLogValidator()
        {
            RuleFor(x => x.LoanRequestLog)
                .NotNull()
                .WithMessage("LoanRequestLog data is required.");

            RuleFor(x => x.LoanRequestLog.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");

            RuleFor(x => x.LoanRequestLog.LoanRequestId)
                .GreaterThan(0)
                .WithMessage("LoanRequestId must be greater than 0.");

            RuleFor(x => x.LoanRequestLog.Action)
                .NotEmpty()
                .WithMessage("Action is required.")
                .MaximumLength(50)
                .WithMessage("Action must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanRequestLog.Action));

            RuleFor(x => x.LoanRequestLog.ActionBy)
                .MaximumLength(100)
                .WithMessage("ActionBy must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanRequestLog.ActionBy));
        }
    }

    public class DeleteLoanRequestLogValidator : AbstractValidator<DeleteLoanRequestLogCommand>
    {
        public DeleteLoanRequestLogValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");
        }
    }
}
