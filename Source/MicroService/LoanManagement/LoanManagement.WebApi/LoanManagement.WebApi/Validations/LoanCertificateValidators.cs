using FluentValidation;
using LoanManagement.Service.Handler.Commands.LoanCertificate;

namespace LoanManagement.WebApi.Validations
{
    public class CreateLoanCertificateValidator : AbstractValidator<CreateLoanCertificateCommand>
    {
        public CreateLoanCertificateValidator()
        {
            RuleFor(x => x.LoanCertificate)
                .NotNull()
                .WithMessage("LoanCertificate data is required.");

            RuleFor(x => x.LoanCertificate.LoanRequestId)
                .GreaterThan(0)
                .WithMessage("LoanRequestId must be greater than 0.");

            RuleFor(x => x.LoanCertificate.CertificateNumber)
                .NotEmpty()
                .WithMessage("CertificateNumber is required.")
                .MaximumLength(50)
                .WithMessage("CertificateNumber must not exceed 50 characters.");
        }
    }

    public class UpdateLoanCertificateValidator : AbstractValidator<UpdateLoanCertificateCommand>
    {
        public UpdateLoanCertificateValidator()
        {
            RuleFor(x => x.LoanCertificate)
                .NotNull()
                .WithMessage("LoanCertificate data is required.");

            RuleFor(x => x.LoanCertificate.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");

            RuleFor(x => x.LoanCertificate.LoanRequestId)
                .GreaterThan(0)
                .WithMessage("LoanRequestId must be greater than 0.");

            RuleFor(x => x.LoanCertificate.CertificateNumber)
                .NotEmpty()
                .WithMessage("CertificateNumber is required.")
                .MaximumLength(50)
                .WithMessage("CertificateNumber must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanCertificate.CertificateNumber));
        }
    }

    public class DeleteLoanCertificateValidator : AbstractValidator<DeleteLoanCertificateCommand>
    {
        public DeleteLoanCertificateValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");
        }
    }
}
