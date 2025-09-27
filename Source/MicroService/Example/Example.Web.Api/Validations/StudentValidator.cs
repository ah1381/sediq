using Example.Service.Models.DTOs;
using FluentValidation;

namespace Example.Web.Api.Validations
{
    public class StudentCreateValidator : AbstractValidator<StudentCreateDto>
    {
        public StudentCreateValidator()
        {
            RuleFor(x => x.StudentCode)
                .GreaterThan(0).WithMessage("کد دانش‌آموز باید بزرگتر از صفر باشد.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("وارد کردن نام الزامی است.")
                .MaximumLength(50).WithMessage("نام نمی‌تواند بیش از 50 کاراکتر باشد.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("وارد کردن نام خانوادگی الزامی است.")
                .MaximumLength(50).WithMessage("نام خانوادگی نمی‌تواند بیش از 50 کاراکتر باشد.");

            RuleFor(x => x.NationalCode)
                .NotEmpty().WithMessage("کد ملی الزامی است.")
                .Length(10).WithMessage("کد ملی باید 10 رقم باشد.");

            RuleFor(x => x.BirthDate)
                .LessThan(DateTime.Now).WithMessage("تاریخ تولد نمی‌تواند بعد از امروز باشد.");

            RuleFor(x => x.MembershipDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("تاریخ عضویت نمی‌تواند بعد از امروز باشد.");

            RuleFor(x => x.FieldOfStudy)
                .NotEmpty().WithMessage("رشته تحصیلی الزامی است.");

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("جنسیت معتبر نیست.");

            RuleForEach(x => x.Photos)
                .NotEmpty().WithMessage("آدرس عکس نمی‌تواند خالی باشد.");

            RuleForEach(x => x.PhoneNumbers)
                .SetValidator(new PhoneNumberCreateValidator());
        }
    }

    public class StudentEditValidator : AbstractValidator<StudentEditDto>
    {
        public StudentEditValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("شناسه دانش‌آموز معتبر نیست.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("وارد کردن نام الزامی است.")
                .MaximumLength(50).WithMessage("نام نمی‌تواند بیش از 50 کاراکتر باشد.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("وارد کردن نام خانوادگی الزامی است.")
                .MaximumLength(50).WithMessage("نام خانوادگی نمی‌تواند بیش از 50 کاراکتر باشد.");

            RuleFor(x => x.NationalCode)
                .NotEmpty().WithMessage("کد ملی الزامی است.")
                .Length(10).WithMessage("کد ملی باید 10 رقم باشد.");

            RuleFor(x => x.BirthDate)
                .LessThan(DateTime.Now).WithMessage("تاریخ تولد نمی‌تواند بعد از امروز باشد.");

            RuleFor(x => x.MembershipDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("تاریخ عضویت نمی‌تواند بعد از امروز باشد.");

            RuleFor(x => x.FieldOfStudy)
                .NotEmpty().WithMessage("رشته تحصیلی الزامی است.");

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("جنسیت معتبر نیست.");

            RuleForEach(x => x.Photos)
                .NotEmpty().WithMessage("آدرس عکس نمی‌تواند خالی باشد.");
          
            RuleForEach(x => x.PhoneNumbers)
                .SetValidator(new PhoneNumberEditValidator());
        }
    }

    public class StudentDeleteValidator : AbstractValidator<StudentDeleteDto>
    {
        public StudentDeleteValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("شناسه دانش‌آموز معتبر نیست.");
        }
    }
}
