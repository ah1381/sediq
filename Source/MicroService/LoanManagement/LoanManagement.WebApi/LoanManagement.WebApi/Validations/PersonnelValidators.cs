using FluentValidation;
using LoanManagement.Service.Handler.Commands.Personnel;

namespace LoanManagement.WebApi.Validations
{
    public class CreatePersonnelValidator : AbstractValidator<CreatePersonnelCommand>
    {
        public CreatePersonnelValidator()
        {
            RuleFor(x => x.Personnel)
                .NotNull()
                .WithMessage("Personnel data is required.");

            RuleFor(x => x.Personnel.FirstName)
                .NotEmpty()
                .WithMessage("FirstName is required.")
                .MaximumLength(100)
                .WithMessage("FirstName must not exceed 100 characters.");

            RuleFor(x => x.Personnel.LastName)
                .NotEmpty()
                .WithMessage("LastName is required.")
                .MaximumLength(100)
                .WithMessage("LastName must not exceed 100 characters.");

            RuleFor(x => x.Personnel.NationalCode)
                .NotEmpty()
                .WithMessage("NationalCode is required.")
                .Length(10)
                .WithMessage("NationalCode must be exactly 10 digits.")
                .Matches(@"^\d{10}$")
                .WithMessage("NationalCode must contain only digits.");

            RuleFor(x => x.Personnel.EmploymentCode)
                .NotEmpty()
                .WithMessage("EmploymentCode is required.")
                .MaximumLength(50)
                .WithMessage("EmploymentCode must not exceed 50 characters.");

            RuleFor(x => x.Personnel.BirthDate)
                .LessThan(DateTime.Today)
                .WithMessage("BirthDate must be in the past.")
                .When(x => x.Personnel.BirthDate.HasValue);

            RuleFor(x => x.Personnel.Phone)
                .MaximumLength(20)
                .WithMessage("Phone must not exceed 20 characters.")
                .Matches(@"^\+?[\d\s\-\(\)]+$")
                .WithMessage("Phone must contain only digits, spaces, dashes, or parentheses.")
                .When(x => !string.IsNullOrEmpty(x.Personnel.Phone));

            RuleFor(x => x.Personnel.Mobile)
                .MaximumLength(20)
                .WithMessage("Mobile must not exceed 20 characters.")
                .Matches(@"^\+?[\d\s\-\(\)]+$")
                .WithMessage("Mobile must contain only digits, spaces, dashes, or parentheses.")
                .When(x => !string.IsNullOrEmpty(x.Personnel.Mobile));

            RuleFor(x => x.Personnel.Email)
                .EmailAddress()
                .WithMessage("Invalid email format.")
                .MaximumLength(100)
                .WithMessage("Email must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Personnel.Email));

            RuleFor(x => x.Personnel.FatherName)
                .MaximumLength(100)
                .WithMessage("FatherName must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Personnel.FatherName));

            RuleFor(x => x.Personnel.Department)
                .MaximumLength(100)
                .WithMessage("Department must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Personnel.Department));

            RuleFor(x => x.Personnel.Province)
                .MaximumLength(100)
                .WithMessage("Province must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Personnel.Province));

            RuleFor(x => x.Personnel.EmploymentType)
                .MaximumLength(50)
                .WithMessage("EmploymentType must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.Personnel.EmploymentType));
        }
    }

    public class UpdatePersonnelValidator : AbstractValidator<UpdatePersonnelCommand>
    {
        public UpdatePersonnelValidator()
        {
            RuleFor(x => x.Personnel)
                .NotNull()
                .WithMessage("Personnel data is required.");

            RuleFor(x => x.Personnel.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");

            RuleFor(x => x.Personnel.FirstName)
                .NotEmpty()
                .WithMessage("FirstName is required.")
                .MaximumLength(100)
                .WithMessage("FirstName must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Personnel.FirstName));

            RuleFor(x => x.Personnel.LastName)
                .NotEmpty()
                .WithMessage("LastName is required.")
                .MaximumLength(100)
                .WithMessage("LastName must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Personnel.LastName));

            RuleFor(x => x.Personnel.NationalCode)
                .Length(10)
                .WithMessage("NationalCode must be exactly 10 digits.")
                .Matches(@"^\d{10}$")
                .WithMessage("NationalCode must contain only digits.")
                .When(x => !string.IsNullOrEmpty(x.Personnel.NationalCode));

            RuleFor(x => x.Personnel.EmploymentCode)
                .NotEmpty()
                .WithMessage("EmploymentCode is required.")
                .MaximumLength(50)
                .WithMessage("EmploymentCode must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.Personnel.EmploymentCode));

            RuleFor(x => x.Personnel.BirthDate)
                .LessThan(DateTime.Today)
                .WithMessage("BirthDate must be in the past.")
                .When(x => x.Personnel.BirthDate.HasValue);

            RuleFor(x => x.Personnel.Phone)
                .MaximumLength(20)
                .WithMessage("Phone must not exceed 20 characters.")
                .Matches(@"^\+?[\d\s\-\(\)]+$")
                .WithMessage("Phone must contain only digits, spaces, dashes, or parentheses.")
                .When(x => !string.IsNullOrEmpty(x.Personnel.Phone));

            RuleFor(x => x.Personnel.Mobile)
                .MaximumLength(20)
                .WithMessage("Mobile must not exceed 20 characters.")
                .Matches(@"^\+?[\d\s\-\(\)]+$")
                .WithMessage("Mobile must contain only digits, spaces, dashes, or parentheses.")
                .When(x => !string.IsNullOrEmpty(x.Personnel.Mobile));

            RuleFor(x => x.Personnel.Email)
                .EmailAddress()
                .WithMessage("Invalid email format.")
                .MaximumLength(100)
                .WithMessage("Email must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Personnel.Email));

            RuleFor(x => x.Personnel.FatherName)
                .MaximumLength(100)
                .WithMessage("FatherName must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Personnel.FatherName));

            RuleFor(x => x.Personnel.Department)
                .MaximumLength(100)
                .WithMessage("Department must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Personnel.Department));

            RuleFor(x => x.Personnel.Province)
                .MaximumLength(100)
                .WithMessage("Province must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Personnel.Province));

            RuleFor(x => x.Personnel.EmploymentType)
                .MaximumLength(50)
                .WithMessage("EmploymentType must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.Personnel.EmploymentType));
        }
    }

    public class DeletePersonnelValidator : AbstractValidator<DeletePersonnelCommand>
    {
        public DeletePersonnelValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0)
                .WithMessage("RowId must be greater than 0.");
        }
    }
}