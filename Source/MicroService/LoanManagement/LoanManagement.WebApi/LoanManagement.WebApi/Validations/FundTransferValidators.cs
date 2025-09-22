using FluentValidation;
using LoanManagement.Service.Handler.Commands.FundTransfer;

namespace LoanManagement.WebApi.Validations
{
    public class CreateFundTransferValidator : AbstractValidator<CreateFundTransferCommand>
    {
        public CreateFundTransferValidator()
        {
            RuleFor(x => x.FundTransfer)
                .NotNull()
                .WithMessage("FundTransfer data is required.");

            RuleFor(x => x.FundTransfer.MemberId)
                .GreaterThan(0)
                .WithMessage("MemberId must be greater than 0.");

            RuleFor(x => x.FundTransfer.FromFundId)
                .GreaterThan(0)
                .WithMessage("FromFundId must be greater than 0.");

            RuleFor(x => x.FundTransfer.ToFundId)
                .GreaterThan(0)
                .WithMessage("ToFundId must be greater than 0.");

            RuleFor(x => x.FundTransfer.Amount)
                .GreaterThan(0)
                .WithMessage("Amount must be greater than 0.");

            RuleFor(x => x.FundTransfer.CreatedBy)
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundTransfer.CreatedBy));
        }
    }

    public class UpdateFundTransferValidator : AbstractValidator<UpdateFundTransferCommand>
    {
        public UpdateFundTransferValidator()
        {
            RuleFor(x => x.FundTransfer)
                .NotNull()
                .WithMessage("FundTransfer data is required.");

            RuleFor(x => x.FundTransfer.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");

            RuleFor(x => x.FundTransfer.MemberId)
                .GreaterThan(0)
                .WithMessage("MemberId must be greater than 0.");

            RuleFor(x => x.FundTransfer.FromFundId)
                .GreaterThan(0)
                .WithMessage("FromFundId must be greater than 0.");

            RuleFor(x => x.FundTransfer.ToFundId)
                .GreaterThan(0)
                .WithMessage("ToFundId must be greater than 0.");

            RuleFor(x => x.FundTransfer.Amount)
                .GreaterThan(0)
                .WithMessage("Amount must be greater than 0.");

            RuleFor(x => x.FundTransfer.CreatedBy)
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundTransfer.CreatedBy));
        }
    }

    public class DeleteFundTransferValidator : AbstractValidator<DeleteFundTransferCommand>
    {
        public DeleteFundTransferValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");
        }
    }
}