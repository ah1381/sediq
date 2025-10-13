using Example.Service.Models.DTOs;
using FluentValidation;

namespace Example.Web.Api.Validations
{
    public class StudentCreateValidator : AbstractValidator<StudentCreateModel>
    {
        public StudentCreateValidator()
        {


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


            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("جنسیت معتبر نیست.");


            RuleForEach(x => x.PhoneNumbers)
                .SetValidator(new PhoneNumberCreateValidator());

        }
    }

    public class StudentEditValidator : AbstractValidator<StudentUpdateModel>
    {
        public StudentEditValidator()
        {
            RuleFor(x => x.RowId)
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


            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("جنسیت معتبر نیست.");
        }
    }

    public class StudentDeleteValidator : AbstractValidator<StudentDeleteModel>
    {
        public StudentDeleteValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0).WithMessage("شناسه دانش‌آموز معتبر نیست.");
        }
    }
}
