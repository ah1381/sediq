using FluentValidation;
using LoanManagement.Service.Handler.Commands.FundInspector;

namespace LoanManagement.WebApi.Validations
{
    public class CreateFundInspectorValidator : AbstractValidator<CreateFundInspectorCommand>
    {
        public CreateFundInspectorValidator()
        {
            RuleFor(x => x.FundInspector)
                .NotNull()
                .WithMessage("FundInspector data is required.");

            RuleFor(x => x.FundInspector.FundId)
                .GreaterThan(0)
                .WithMessage("FundId must be greater than 0.");

            RuleFor(x => x.FundInspector.PersonnelId)
                .GreaterThan(0)
                .WithMessage("PersonnelId must be greater than 0.");

            RuleFor(x => x.FundInspector.ReportFrequency)
                .MaximumLength(50)
                .WithMessage("ReportFrequency must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundInspector.ReportFrequency));

            RuleFor(x => x.FundInspector.CreatedBy)
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundInspector.CreatedBy));
        }
    }

    public class UpdateFundInspectorValidator : AbstractValidator<UpdateFundInspectorCommand>
    {
        public UpdateFundInspectorValidator()
        {
            RuleFor(x => x.FundInspector)
                .NotNull()
                .WithMessage("FundInspector data is required.");

            RuleFor(x => x.FundInspector.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");

            RuleFor(x => x.FundInspector.FundId)
                .GreaterThan(0)
                .WithMessage("FundId must be greater than 0.");

            RuleFor(x => x.FundInspector.PersonnelId)
                .GreaterThan(0)
                .WithMessage("PersonnelId must be greater than 0.");

            RuleFor(x => x.FundInspector.ReportFrequency)
                .MaximumLength(50)
                .WithMessage("ReportFrequency must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundInspector.ReportFrequency));

            RuleFor(x => x.FundInspector.CreatedBy)
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundInspector.CreatedBy));
        }
    }

    public class DeleteFundInspectorValidator : AbstractValidator<DeleteFundInspectorCommand>
    {
        public DeleteFundInspectorValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");
        }
    }
}