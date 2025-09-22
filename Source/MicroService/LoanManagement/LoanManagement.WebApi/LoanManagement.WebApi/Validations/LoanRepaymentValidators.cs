using FluentValidation;
using LoanManagement.Service.Handler.Commands.LoanRepayment;

namespace LoanManagement.WebApi.Validations
{
    public class CreateLoanRepaymentValidator : AbstractValidator<CreateLoanRepaymentCommand>
    {
        public CreateLoanRepaymentValidator()
        {
            RuleFor(x => x.LoanRepayment)
                .NotNull()
                .WithMessage("LoanRepayment data is required.");

            RuleFor(x => x.LoanRepayment.LoanRequestId)
                .GreaterThan(0)
                .WithMessage("LoanRequestId must be greater than 0.");

            RuleFor(x => x.LoanRepayment.Amount)
                .GreaterThan(0)
                .WithMessage("Amount must be greater than 0.");

            RuleFor(x => x.LoanRepayment.Balance)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Balance must be greater than or equal to 0.");

            RuleFor(x => x.LoanRepayment.ReceiptNumber)
                .MaximumLength(50)
                .WithMessage("ReceiptNumber must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanRepayment.ReceiptNumber));

            RuleFor(x => x.LoanRepayment.CreatedBy)
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanRepayment.CreatedBy));

            RuleFor(x => x.LoanRepayment.PaymentMethod)
                .MaximumLength(50)
                .WithMessage("PaymentMethod must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanRepayment.PaymentMethod));

            RuleFor(x => x.LoanRepayment.PaymentGatewayRef)
                .MaximumLength(100)
                .WithMessage("PaymentGatewayRef must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanRepayment.PaymentGatewayRef));
        }
    }

    public class UpdateLoanRepaymentValidator : AbstractValidator<UpdateLoanRepaymentCommand>
    {
        public UpdateLoanRepaymentValidator()
        {
            RuleFor(x => x.LoanRepayment)
                .NotNull()
                .WithMessage("LoanRepayment data is required.");

            RuleFor(x => x.LoanRepayment.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");

            RuleFor(x => x.LoanRepayment.LoanRequestId)
                .GreaterThan(0)
                .WithMessage("LoanRequestId must be greater than 0.");

            RuleFor(x => x.LoanRepayment.Amount)
                .GreaterThan(0)
                .WithMessage("Amount must be greater than 0.");

            RuleFor(x => x.LoanRepayment.Balance)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Balance must be greater than or equal to 0.");

            RuleFor(x => x.LoanRepayment.ReceiptNumber)
                .MaximumLength(50)
                .WithMessage("ReceiptNumber must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanRepayment.ReceiptNumber));

            RuleFor(x => x.LoanRepayment.CreatedBy)
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanRepayment.CreatedBy));

            RuleFor(x => x.LoanRepayment.PaymentMethod)
                .MaximumLength(50)
                .WithMessage("PaymentMethod must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanRepayment.PaymentMethod));

            RuleFor(x => x.LoanRepayment.PaymentGatewayRef)
                .MaximumLength(100)
                .WithMessage("PaymentGatewayRef must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanRepayment.PaymentGatewayRef));
        }
    }

    public class DeleteLoanRepaymentValidator : AbstractValidator<DeleteLoanRepaymentCommand>
    {
        public DeleteLoanRepaymentValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");
        }
    }
}