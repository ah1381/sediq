using Example.Service.Models.DTOs;
using FluentValidation;

namespace Example.Web.Api.Validations
{
    public class PhoneNumberCreateValidator : AbstractValidator<PhoneNumberCreateDto>
    {
        public PhoneNumberCreateValidator()
        {
            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("وارد کردن شماره تلفن الزامی است.")
                .MaximumLength(20).WithMessage("شماره تلفن نمی‌تواند بیشتر از 20 کاراکتر باشد.");

            RuleFor(x => x.Ownership)
                .NotEmpty().WithMessage("انتخاب مالکیت الزامی است.");

            RuleFor(x => x.StudentId)
                .GreaterThan(0).WithMessage("شناسه دانش‌آموز معتبر نیست.");
        }
    }

    public class PhoneNumberEditValidator : AbstractValidator<PhoneNumberEditDto>
    {
        public PhoneNumberEditValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("شناسه شماره تلفن معتبر نیست.");

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("وارد کردن شماره تلفن الزامی است.")
                .MaximumLength(20).WithMessage("شماره تلفن نمی‌تواند بیشتر از 20 کاراکتر باشد.");

            RuleFor(x => x.Ownership)
                .NotEmpty().WithMessage("انتخاب مالکیت الزامی است.");

            RuleFor(x => x.StudentId)
                .GreaterThan(0).WithMessage("شناسه دانش‌آموز معتبر نیست.");
        }
    }

    public class PhoneNumberDeleteValidator : AbstractValidator<PhoneNumberDeleteDto>
    {
        public PhoneNumberDeleteValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("شناسه شماره تلفن معتبر نیست.");
        }
    }
}
