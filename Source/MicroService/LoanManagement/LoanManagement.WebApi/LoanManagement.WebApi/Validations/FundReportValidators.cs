using FluentValidation;
using LoanManagement.Service.Handler.Commands.FundReport;

namespace LoanManagement.WebApi.Validations
{
    public class CreateFundReportValidator : AbstractValidator<CreateFundReportCommand>
    {
        public CreateFundReportValidator()
        {
            RuleFor(x => x.FundReport)
                .NotNull()
                .WithMessage("FundReport data is required.");

            RuleFor(x => x.FundReport.FundId)
                .GreaterThan(0)
                .WithMessage("FundId must be greater than 0.");

            RuleFor(x => x.FundReport.ReportYear)
                .GreaterThan(1900)
                .WithMessage("ReportYear must be greater than 1900.")
                .LessThanOrEqualTo(DateTime.Now.Year + 1)
                .WithMessage($"ReportYear must not exceed {DateTime.Now.Year + 1}.");

            RuleFor(x => x.FundReport.ReportType)
                .NotEmpty()
                .WithMessage("ReportType is required.")
                .MaximumLength(50)
                .WithMessage("ReportType must not exceed 50 characters.");

            RuleFor(x => x.FundReport.Balance)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Balance must be greater than or equal to 0.");

            RuleFor(x => x.FundReport.CreatedBy)
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundReport.CreatedBy));
        }
    }

    public class UpdateFundReportValidator : AbstractValidator<UpdateFundReportCommand>
    {
        public UpdateFundReportValidator()
        {
            RuleFor(x => x.FundReport)
                .NotNull()
                .WithMessage("FundReport data is required.");

            RuleFor(x => x.FundReport.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");

            RuleFor(x => x.FundReport.FundId)
                .GreaterThan(0)
                .WithMessage("FundId must be greater than 0.");

            RuleFor(x => x.FundReport.ReportYear)
                .GreaterThan(1900)
                .WithMessage("ReportYear must be greater than 1900.")
                .LessThanOrEqualTo(DateTime.Now.Year + 1)
                .WithMessage($"ReportYear must not exceed {DateTime.Now.Year + 1}.");

            RuleFor(x => x.FundReport.ReportType)
                .NotEmpty()
                .WithMessage("ReportType is required.")
                .MaximumLength(50)
                .WithMessage("ReportType must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundReport.ReportType));

            RuleFor(x => x.FundReport.Balance)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Balance must be greater than or equal to 0.");

            RuleFor(x => x.FundReport.CreatedBy)
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundReport.CreatedBy));
        }
    }

    public class DeleteFundReportValidator : AbstractValidator<DeleteFundReportCommand>
    {
        public DeleteFundReportValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");
        }
    }
}