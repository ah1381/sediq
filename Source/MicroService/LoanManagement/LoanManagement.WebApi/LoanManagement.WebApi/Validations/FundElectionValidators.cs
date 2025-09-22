using FluentValidation;
using LoanManagement.Service.Handler.Commands.FundElection;

namespace LoanManagement.WebApi.Validations
{
    public class CreateFundElectionValidator : AbstractValidator<CreateFundElectionCommand>
    {
        public CreateFundElectionValidator()
        {
            RuleFor(x => x.FundElection)
                .NotNull()
                .WithMessage("FundElection data is required.");

            RuleFor(x => x.FundElection.FundId)
                .GreaterThan(0)
                .WithMessage("FundId must be greater than 0.");

            RuleFor(x => x.FundElection.CandidateId)
                .GreaterThan(0)
                .WithMessage("CandidateId must be greater than 0.");

            RuleFor(x => x.FundElection.Position)
                .NotEmpty()
                .WithMessage("Position is required.")
                .MaximumLength(50)
                .WithMessage("Position must not exceed 50 characters.");

            RuleFor(x => x.FundElection.Votes)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Votes must be greater than or equal to 0.");

            RuleFor(x => x.FundElection.CreatedBy)
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundElection.CreatedBy));
        }
    }

    public class UpdateFundElectionValidator : AbstractValidator<UpdateFundElectionCommand>
    {
        public UpdateFundElectionValidator()
        {
            RuleFor(x => x.FundElection)
                .NotNull()
                .WithMessage("FundElection data is required.");

            RuleFor(x => x.FundElection.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");

            RuleFor(x => x.FundElection.FundId)
                .GreaterThan(0)
                .WithMessage("FundId must be greater than 0.");

            RuleFor(x => x.FundElection.CandidateId)
                .GreaterThan(0)
                .WithMessage("CandidateId must be greater than 0.");

            RuleFor(x => x.FundElection.Position)
                .NotEmpty()
                .WithMessage("Position is required.")
                .MaximumLength(50)
                .WithMessage("Position must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundElection.Position));

            RuleFor(x => x.FundElection.Votes)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Votes must be greater than or equal to 0.");

            RuleFor(x => x.FundElection.CreatedBy)
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundElection.CreatedBy));
        }
    }

    public class DeleteFundElectionValidator : AbstractValidator<DeleteFundElectionCommand>
    {
        public DeleteFundElectionValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");
        }
    }
}
