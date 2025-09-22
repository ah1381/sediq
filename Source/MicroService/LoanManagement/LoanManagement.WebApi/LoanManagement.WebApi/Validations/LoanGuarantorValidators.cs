using FluentValidation;
using LoanManagement.Service.Handler.Commands.LoanGuarantor;

namespace LoanManagement.WebApi.Validations
{
    public class CreateLoanGuarantorValidator : AbstractValidator<CreateLoanGuarantorCommand>
    {
        public CreateLoanGuarantorValidator()
        {
            RuleFor(x => x.LoanGuarantor)
                .NotNull()
                .WithMessage("LoanGuarantor data is required.");

            RuleFor(x => x.LoanGuarantor.LoanRequestId)
                .GreaterThan(0)
                .WithMessage("LoanRequestId must be greater than 0.");

            RuleFor(x => x.LoanGuarantor.PersonnelId)
                .GreaterThan(0)
                .WithMessage("PersonnelId must be greater than 0.");

            RuleFor(x => x.LoanGuarantor.GuarantorCode)
                .NotEmpty()
                .WithMessage("GuarantorCode is required.")
                .MaximumLength(50)
                .WithMessage("GuarantorCode must not exceed 50 characters.");

            RuleFor(x => x.LoanGuarantor.CreatedBy)
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanGuarantor.CreatedBy));
        }
    }

    public class UpdateLoanGuarantorValidator : AbstractValidator<UpdateLoanGuarantorCommand>
    {
        public UpdateLoanGuarantorValidator()
        {
            RuleFor(x => x.LoanGuarantor)
                .NotNull()
                .WithMessage("LoanGuarantor data is required.");

            RuleFor(x => x.LoanGuarantor.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");

            RuleFor(x => x.LoanGuarantor.LoanRequestId)
                .GreaterThan(0)
                .WithMessage("LoanRequestId must be greater than 0.");

            RuleFor(x => x.LoanGuarantor.PersonnelId)
                .GreaterThan(0)
                .WithMessage("PersonnelId must be greater than 0.");

            RuleFor(x => x.LoanGuarantor.GuarantorCode)
                .NotEmpty()
                .WithMessage("GuarantorCode is required.")
                .MaximumLength(50)
                .WithMessage("GuarantorCode must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanGuarantor.GuarantorCode));

            RuleFor(x => x.LoanGuarantor.CreatedBy)
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanGuarantor.CreatedBy));
        }
    }

    public class DeleteLoanGuarantorValidator : AbstractValidator<DeleteLoanGuarantorCommand>
    {
        public DeleteLoanGuarantorValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");
        }
    }
}
