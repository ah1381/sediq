using FluentValidation;
using LoanManagement.Service.Handler.Commands.LoanRequest;

namespace LoanManagement.WebApi.Validations
{
    public class CreateLoanRequestValidator : AbstractValidator<CreateLoanRequestCommand>
    {
        public CreateLoanRequestValidator()
        {
            RuleFor(x => x.LoanRequest)
                .NotNull()
                .WithMessage("LoanRequest data is required.");

            RuleFor(x => x.LoanRequest.PersonnelId)
                .GreaterThan(0)
                .WithMessage("PersonnelId must be greater than 0.");

            RuleFor(x => x.LoanRequest.LoanTypeId)
                .GreaterThan(0)
                .WithMessage("LoanTypeId must be greater than 0.");

            RuleFor(x => x.LoanRequest.FundId)
                .GreaterThan(0)
                .WithMessage("FundId must be greater than 0.");

            RuleFor(x => x.LoanRequest.AmountRequested)
                .GreaterThan(0)
                .WithMessage("AmountRequested must be greater than 0.");

            RuleFor(x => x.LoanRequest.MaxLoanAmount)
                .GreaterThan(0)
                .WithMessage("MaxLoanAmount must be greater than 0.");

            RuleFor(x => x.LoanRequest.WaitingMonths)
                .GreaterThanOrEqualTo(0)
                .WithMessage("WaitingMonths must be greater than or equal to 0.");

            RuleFor(x => x.LoanRequest.Installments)
                .GreaterThan(0)
                .WithMessage("Installments must be greater than 0.");

            RuleFor(x => x.LoanRequest.MonthlyInstallment)
                .GreaterThan(0)
                .WithMessage("MonthlyInstallment must be greater than 0.");

            RuleFor(x => x.LoanRequest.BankAccount)
                .MaximumLength(50)
                .WithMessage("BankAccount must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanRequest.BankAccount));

            RuleFor(x => x.LoanRequest.Address)
                .MaximumLength(200)
                .WithMessage("Address must not exceed 200 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanRequest.Address));

            RuleFor(x => x.LoanRequest.Phone)
                .MaximumLength(20)
                .WithMessage("Phone must not exceed 20 characters.")
                .Matches(@"^\+?[\d\s\-\(\)]+$")
                .WithMessage("Phone must contain only digits, spaces, dashes, or parentheses.")
                .When(x => !string.IsNullOrEmpty(x.LoanRequest.Phone));

            RuleFor(x => x.LoanRequest.Mobile)
                .MaximumLength(20)
                .WithMessage("Mobile must not exceed 20 characters.")
                .Matches(@"^\+?[\d\s\-\(\)]+$")
                .WithMessage("Mobile must contain only digits, spaces, dashes, or parentheses.")
                .When(x => !string.IsNullOrEmpty(x.LoanRequest.Mobile));

            RuleFor(x => x.LoanRequest.CreatedBy)
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanRequest.CreatedBy));

            RuleFor(x => x.LoanRequest.Province)
                .MaximumLength(100)
                .WithMessage("Province must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanRequest.Province));

            RuleFor(x => x.LoanRequest.Region)
                .MaximumLength(100)
                .WithMessage("Region must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanRequest.Region));

            RuleFor(x => x.LoanRequest.Department)
                .MaximumLength(100)
                .WithMessage("Department must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanRequest.Department));

            RuleFor(x => x.LoanRequest.EmploymentType)
                .MaximumLength(50)
                .WithMessage("EmploymentType must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanRequest.EmploymentType));
        }
    }

    public class UpdateLoanRequestValidator : AbstractValidator<UpdateLoanRequestCommand>
    {
        public UpdateLoanRequestValidator()
        {
            RuleFor(x => x.LoanRequest)
                .NotNull()
                .WithMessage("LoanRequest data is required.");

            RuleFor(x => x.LoanRequest.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");

            RuleFor(x => x.LoanRequest.PersonnelId)
                .GreaterThan(0)
                .WithMessage("PersonnelId must be greater than 0.");

            RuleFor(x => x.LoanRequest.LoanTypeId)
                .GreaterThan(0)
                .WithMessage("LoanTypeId must be greater than 0.");

            RuleFor(x => x.LoanRequest.FundId)
                .GreaterThan(0)
                .WithMessage("FundId must be greater than 0.");

            RuleFor(x => x.LoanRequest.AmountRequested)
                .GreaterThan(0)
                .WithMessage("AmountRequested must be greater than 0.");

            RuleFor(x => x.LoanRequest.MaxLoanAmount)
                .GreaterThan(0)
                .WithMessage("MaxLoanAmount must be greater than 0.");

            RuleFor(x => x.LoanRequest.WaitingMonths)
                .GreaterThanOrEqualTo(0)
                .WithMessage("WaitingMonths must be greater than or equal to 0.");

            RuleFor(x => x.LoanRequest.Installments)
                .GreaterThan(0)
                .WithMessage("Installments must be greater than 0.");

            RuleFor(x => x.LoanRequest.MonthlyInstallment)
                .GreaterThan(0)
                .WithMessage("MonthlyInstallment must be greater than 0.");

            RuleFor(x => x.LoanRequest.BankAccount)
                .MaximumLength(50)
                .WithMessage("BankAccount must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanRequest.BankAccount));

            RuleFor(x => x.LoanRequest.Address)
                .MaximumLength(200)
                .WithMessage("Address must not exceed 200 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanRequest.Address));

            RuleFor(x => x.LoanRequest.Phone)
                .MaximumLength(20)
                .WithMessage("Phone must not exceed 20 characters.")
                .Matches(@"^\+?[\d\s\-\(\)]+$")
                .WithMessage("Phone must contain only digits, spaces, dashes, or parentheses.")
                .When(x => !string.IsNullOrEmpty(x.LoanRequest.Phone));

            RuleFor(x => x.LoanRequest.Mobile)
                .MaximumLength(20)
                .WithMessage("Mobile must not exceed 20 characters.")
                .Matches(@"^\+?[\d\s\-\(\)]+$")
                .WithMessage("Mobile must contain only digits, spaces, dashes, or parentheses.")
                .When(x => !string.IsNullOrEmpty(x.LoanRequest.Mobile));

            RuleFor(x => x.LoanRequest.CreatedBy)
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanRequest.CreatedBy));

            RuleFor(x => x.LoanRequest.Province)
                .MaximumLength(100)
                .WithMessage("Province must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanRequest.Province));

            RuleFor(x => x.LoanRequest.Region)
                .MaximumLength(100)
                .WithMessage("Region must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanRequest.Region));

            RuleFor(x => x.LoanRequest.Department)
                .MaximumLength(100)
                .WithMessage("Department must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanRequest.Department));

            RuleFor(x => x.LoanRequest.EmploymentType)
                .MaximumLength(50)
                .WithMessage("EmploymentType must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.LoanRequest.EmploymentType));
        }
    }

    public class DeleteLoanRequestValidator : AbstractValidator<DeleteLoanRequestCommand>
    {
        public DeleteLoanRequestValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");
        }
    }
}