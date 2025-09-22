using FluentValidation;
using LoanManagement.Service.Handler.Commands.LoanAdjustmentRequest;

namespace LoanManagement.WebApi.Validations
{
    public class CreateLoanAdjustmentRequestValidator : AbstractValidator<CreateLoanAdjustmentRequestCommand>
    {
        public CreateLoanAdjustmentRequestValidator()
        {
            RuleFor(x => x.LoanAdjustmentRequest)
                .NotNull()
                .WithMessage("LoanAdjustmentRequest data is required.");

            RuleFor(x => x.LoanAdjustmentRequest.MemberId)
                .GreaterThan(0)
                .WithMessage("MemberId must be greater than 0.");

            RuleFor(x => x.LoanAdjustmentRequest.RequestType)
                .NotEmpty()
                .WithMessage("RequestType is required.")
                .MaximumLength(50)
                .WithMessage("RequestType must not exceed 50 characters.");

            RuleFor(x => x.LoanAdjustmentRequest.OldValue)
                .GreaterThanOrEqualTo(0)
                .WithMessage("OldValue must be greater than or equal to 0.");

            RuleFor(x => x.LoanAdjustmentRequest.NewValue)
                .GreaterThanOrEqualTo(0)
                .WithMessage("NewValue must be greater than or equal to 0.");

            RuleFor(x => x.LoanAdjustmentRequest.MaxAllowed)
                .GreaterThanOrEqualTo(0)
                .WithMessage("MaxAllowed must be greater than or equal to 0.");

            RuleFor(x => x.LoanAdjustmentRequest.StatusDesc)
                .MaximumLength(50)
                .WithMessage("StatusDesc must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanAdjustmentRequest.StatusDesc));

            RuleFor(x => x.LoanAdjustmentRequest.CreatedBy)
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanAdjustmentRequest.CreatedBy));
        }
    }

    public class UpdateLoanAdjustmentRequestValidator : AbstractValidator<UpdateLoanAdjustmentRequestCommand>
    {
        public UpdateLoanAdjustmentRequestValidator()
        {
            RuleFor(x => x.LoanAdjustmentRequest)
                .NotNull()
                .WithMessage("LoanAdjustmentRequest data is required.");

            RuleFor(x => x.LoanAdjustmentRequest.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");

            RuleFor(x => x.LoanAdjustmentRequest.MemberId)
                .GreaterThan(0)
                .WithMessage("MemberId must be greater than 0.");

            RuleFor(x => x.LoanAdjustmentRequest.RequestType)
                .NotEmpty()
                .WithMessage("RequestType is required.")
                .MaximumLength(50)
                .WithMessage("RequestType must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanAdjustmentRequest.RequestType));

            RuleFor(x => x.LoanAdjustmentRequest.OldValue)
                .GreaterThanOrEqualTo(0)
                .WithMessage("OldValue must be greater than or equal to 0.");

            RuleFor(x => x.LoanAdjustmentRequest.NewValue)
                .GreaterThanOrEqualTo(0)
                .WithMessage("NewValue must be greater than or equal to 0.");

            RuleFor(x => x.LoanAdjustmentRequest.MaxAllowed)
                .GreaterThanOrEqualTo(0)
                .WithMessage("MaxAllowed must be greater than or equal to 0.");

            RuleFor(x => x.LoanAdjustmentRequest.StatusDesc)
                .MaximumLength(50)
                .WithMessage("StatusDesc must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanAdjustmentRequest.StatusDesc));

            RuleFor(x => x.LoanAdjustmentRequest.CreatedBy)
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanAdjustmentRequest.CreatedBy));
        }
    }

    public class DeleteLoanAdjustmentRequestValidator : AbstractValidator<DeleteLoanAdjustmentRequestCommand>
    {
        public DeleteLoanAdjustmentRequestValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");
        }
    }
}
