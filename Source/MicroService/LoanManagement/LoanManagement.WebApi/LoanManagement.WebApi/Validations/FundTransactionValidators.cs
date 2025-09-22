using FluentValidation;
using LoanManagement.Service.Handler.Commands.FundTransaction;

namespace LoanManagement.WebApi.Validations
{
    public class CreateFundTransactionValidator : AbstractValidator<CreateFundTransactionCommand>
    {
        public CreateFundTransactionValidator()
        {
            RuleFor(x => x.FundTransaction)
                .NotNull()
                .WithMessage("FundTransaction data is required.");

            RuleFor(x => x.FundTransaction.FundId)
                .GreaterThan(0)
                .WithMessage("FundId must be greater than 0.");

            RuleFor(x => x.FundTransaction.MemberId)
                .GreaterThan(0)
                .WithMessage("MemberId must be greater than 0.");

            RuleFor(x => x.FundTransaction.TransactionType)
                .NotEmpty()
                .WithMessage("TransactionType is required.")
                .MaximumLength(50)
                .WithMessage("TransactionType must not exceed 50 characters.");

            RuleFor(x => x.FundTransaction.Amount)
                .GreaterThan(0)
                .WithMessage("Amount must be greater than 0.");

            RuleFor(x => x.FundTransaction.Balance)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Balance must be greater than or equal to 0.");

            RuleFor(x => x.FundTransaction.DebitAmount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("DebitAmount must be greater than or equal to 0.");

            RuleFor(x => x.FundTransaction.CreditAmount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("CreditAmount must be greater than or equal to 0.");

            RuleFor(x => x.FundTransaction.Commission)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Commission must be greater than or equal to 0.");

            RuleFor(x => x.FundTransaction.Insurance)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Insurance must be greater than or equal to 0.");

            RuleFor(x => x.FundTransaction.CreatedBy)
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundTransaction.CreatedBy));
        }
    }

    public class UpdateFundTransactionValidator : AbstractValidator<UpdateFundTransactionCommand>
    {
        public UpdateFundTransactionValidator()
        {
            RuleFor(x => x.FundTransaction)
                .NotNull()
                .WithMessage("FundTransaction data is required.");

            RuleFor(x => x.FundTransaction.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");

            RuleFor(x => x.FundTransaction.FundId)
                .GreaterThan(0)
                .WithMessage("FundId must be greater than 0.");

            RuleFor(x => x.FundTransaction.MemberId)
                .GreaterThan(0)
                .WithMessage("MemberId must be greater than 0.");

            RuleFor(x => x.FundTransaction.TransactionType)
                .NotEmpty()
                .WithMessage("TransactionType is required.")
                .MaximumLength(50)
                .WithMessage("TransactionType must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundTransaction.TransactionType));

            RuleFor(x => x.FundTransaction.Amount)
                .GreaterThan(0)
                .WithMessage("Amount must be greater than 0.");

            RuleFor(x => x.FundTransaction.Balance)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Balance must be greater than or equal to 0.");

            RuleFor(x => x.FundTransaction.DebitAmount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("DebitAmount must be greater than or equal to 0.");

            RuleFor(x => x.FundTransaction.CreditAmount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("CreditAmount must be greater than or equal to 0.");

            RuleFor(x => x.FundTransaction.Commission)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Commission must be greater than or equal to 0.");

            RuleFor(x => x.FundTransaction.Insurance)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Insurance must be greater than or equal to 0.");

            RuleFor(x => x.FundTransaction.CreatedBy)
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundTransaction.CreatedBy));
        }
    }

    public class DeleteFundTransactionValidator : AbstractValidator<DeleteFundTransactionCommand>
    {
        public DeleteFundTransactionValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");
        }
    }
}