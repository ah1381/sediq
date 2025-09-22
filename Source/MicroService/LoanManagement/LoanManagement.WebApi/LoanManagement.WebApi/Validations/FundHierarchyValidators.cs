using FluentValidation;
using LoanManagement.Service.Handler.Commands.FundHierarchy;

namespace LoanManagement.WebApi.Validations
{
    public class CreateFundHierarchyValidator : AbstractValidator<CreateFundHierarchyCommand>
    {
        public CreateFundHierarchyValidator()
        {
            RuleFor(x => x.FundHierarchy)
                .NotNull()
                .WithMessage("FundHierarchy data is required.");

            RuleFor(x => x.FundHierarchy.FundId)
                .GreaterThan(0)
                .WithMessage("FundId must be greater than 0.");

            RuleFor(x => x.FundHierarchy.ParentFundId)
                .GreaterThan(0)
                .WithMessage("ParentFundId must be greater than 0.")
                .When(x => x.FundHierarchy.ParentFundId.HasValue);

            RuleFor(x => x.FundHierarchy.Description)
                .MaximumLength(500)
                .WithMessage("Description must not exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundHierarchy.Description));
        }
    }

    public class UpdateFundHierarchyValidator : AbstractValidator<UpdateFundHierarchyCommand>
    {
        public UpdateFundHierarchyValidator()
        {
            RuleFor(x => x.FundHierarchy)
                .NotNull()
                .WithMessage("FundHierarchy data is required.");

            RuleFor(x => x.FundHierarchy.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");

            RuleFor(x => x.FundHierarchy.FundId)
                .GreaterThan(0)
                .WithMessage("FundId must be greater than 0.");

            RuleFor(x => x.FundHierarchy.ParentFundId)
                .GreaterThan(0)
                .WithMessage("ParentFundId must be greater than 0.")
                .When(x => x.FundHierarchy.ParentFundId.HasValue);

            RuleFor(x => x.FundHierarchy.Description)
                .MaximumLength(500)
                .WithMessage("Description must not exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundHierarchy.Description));
        }
    }

    public class DeleteFundHierarchyValidator : AbstractValidator<DeleteFundHierarchyCommand>
    {
        public DeleteFundHierarchyValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");
        }
    }
}