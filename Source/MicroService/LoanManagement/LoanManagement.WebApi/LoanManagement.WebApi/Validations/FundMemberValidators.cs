using FluentValidation;
using LoanManagement.Service.Handler.Commands.FundMember;

namespace LoanManagement.WebApi.Validations
{
    public class CreateFundMemberValidator : AbstractValidator<CreateFundMemberCommand>
    {
        public CreateFundMemberValidator()
        {
            RuleFor(x => x.FundMember)
                .NotNull()
                .WithMessage("FundMember data is required.");

            RuleFor(x => x.FundMember.PersonnelId)
                .GreaterThan(0)
                .WithMessage("PersonnelId must be greater than 0.");

            RuleFor(x => x.FundMember.FundId)
                .GreaterThan(0)
                .WithMessage("FundId must be greater than 0.");

            RuleFor(x => x.FundMember.MembershipType)
                .NotEmpty()
                .WithMessage("MembershipType is required.")
                .MaximumLength(50)
                .WithMessage("MembershipType must not exceed 50 characters.");

            RuleFor(x => x.FundMember.MembershipNumber)
                .NotEmpty()
                .WithMessage("MembershipNumber is required.")
                .MaximumLength(50)
                .WithMessage("MembershipNumber must not exceed 50 characters.");

            RuleFor(x => x.FundMember.Shares)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Shares must be greater than or equal to 0.");

            RuleFor(x => x.FundMember.TotalSalary)
                .GreaterThanOrEqualTo(0)
                .WithMessage("TotalSalary must be greater than or equal to 0.");

            RuleFor(x => x.FundMember.NetPayment)
                .GreaterThanOrEqualTo(0)
                .WithMessage("NetPayment must be greater than or equal to 0.");

            RuleFor(x => x.FundMember.WaitingMonths)
                .GreaterThanOrEqualTo(0)
                .WithMessage("WaitingMonths must be greater than or equal to 0.");

            RuleFor(x => x.FundMember.BankAccount)
                .MaximumLength(50)
                .WithMessage("BankAccount must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundMember.BankAccount));

            RuleFor(x => x.FundMember.EmploymentType)
                .MaximumLength(50)
                .WithMessage("EmploymentType must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundMember.EmploymentType));

            RuleFor(x => x.FundMember.Position)
                .MaximumLength(50)
                .WithMessage("Position must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundMember.Position));

            RuleFor(x => x.FundMember.Email)
                .EmailAddress()
                .WithMessage("Invalid email format.")
                .MaximumLength(100)
                .WithMessage("Email must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundMember.Email));

            RuleFor(x => x.FundMember.Phone)
                .MaximumLength(20)
                .WithMessage("Phone must not exceed 20 characters.")
                .Matches(@"^\+?[\d\s\-\(\)]+$")
                .WithMessage("Phone must contain only digits, spaces, dashes, or parentheses.")
                .When(x => !string.IsNullOrEmpty(x.FundMember.Phone));

            RuleFor(x => x.FundMember.StatusDesc)
                .MaximumLength(50)
                .WithMessage("StatusDesc must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundMember.StatusDesc));

            RuleFor(x => x.FundMember.CreatedBy)
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundMember.CreatedBy));

            RuleFor(x => x.FundMember.Province)
                .MaximumLength(100)
                .WithMessage("Province must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundMember.Province));
        }
    }

    public class UpdateFundMemberValidator : AbstractValidator<UpdateFundMemberCommand>
    {
        public UpdateFundMemberValidator()
        {
            RuleFor(x => x.FundMember)
                .NotNull()
                .WithMessage("FundMember data is required.");

            RuleFor(x => x.FundMember.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");

            RuleFor(x => x.FundMember.PersonnelId)
                .GreaterThan(0)
                .WithMessage("PersonnelId must be greater than 0.");

            RuleFor(x => x.FundMember.FundId)
                .GreaterThan(0)
                .WithMessage("FundId must be greater than 0.");

            RuleFor(x => x.FundMember.MembershipType)
                .NotEmpty()
                .WithMessage("MembershipType is required.")
                .MaximumLength(50)
                .WithMessage("MembershipType must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundMember.MembershipType));

            RuleFor(x => x.FundMember.MembershipNumber)
                .NotEmpty()
                .WithMessage("MembershipNumber is required.")
                .MaximumLength(50)
                .WithMessage("MembershipNumber must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundMember.MembershipNumber));

            RuleFor(x => x.FundMember.Shares)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Shares must be greater than or equal to 0.");

            RuleFor(x => x.FundMember.TotalSalary)
                .GreaterThanOrEqualTo(0)
                .WithMessage("TotalSalary must be greater than or equal to 0.");

            RuleFor(x => x.FundMember.NetPayment)
                .GreaterThanOrEqualTo(0)
                .WithMessage("NetPayment must be greater than or equal to 0.");

            RuleFor(x => x.FundMember.WaitingMonths)
                .GreaterThanOrEqualTo(0)
                .WithMessage("WaitingMonths must be greater than or equal to 0.");

            RuleFor(x => x.FundMember.BankAccount)
                .MaximumLength(50)
                .WithMessage("BankAccount must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundMember.BankAccount));

            RuleFor(x => x.FundMember.EmploymentType)
                .MaximumLength(50)
                .WithMessage("EmploymentType must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundMember.EmploymentType));

            RuleFor(x => x.FundMember.Position)
                .MaximumLength(50)
                .WithMessage("Position must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundMember.Position));

            RuleFor(x => x.FundMember.Email)
                .EmailAddress()
                .WithMessage("Invalid email format.")
                .MaximumLength(100)
                .WithMessage("Email must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundMember.Email));

            RuleFor(x => x.FundMember.Phone)
                .MaximumLength(20)
                .WithMessage("Phone must not exceed 20 characters.")
                .Matches(@"^\+?[\d\s\-\(\)]+$")
                .WithMessage("Phone must contain only digits, spaces, dashes, or parentheses.")
                .When(x => !string.IsNullOrEmpty(x.FundMember.Phone));

            RuleFor(x => x.FundMember.StatusDesc)
                .MaximumLength(50)
                .WithMessage("StatusDesc must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundMember.StatusDesc));

            RuleFor(x => x.FundMember.CreatedBy)
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundMember.CreatedBy));

            RuleFor(x => x.FundMember.Province)
                .MaximumLength(100)
                .WithMessage("Province must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.FundMember.Province));
        }
    }

    public class DeleteFundMemberValidator : AbstractValidator<DeleteFundMemberCommand>
    {
        public DeleteFundMemberValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");
        }
    }
}