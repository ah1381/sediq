using Example.Service.Models.DTOs;
using FluentValidation;

namespace Example.Web.Api.Validations
{
    public class PhoneNumberCreateValidator : AbstractValidator<PhoneNumberCreateModel>
    {
        public PhoneNumberCreateValidator()
        {
            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("وارد کردن شماره تلفن الزامی است.")
                .MaximumLength(20).WithMessage("شماره تلفن نمی‌تواند بیشتر از 20 کاراکتر باشد.");

            RuleFor(x => x.Ownership)
                .IsInEnum().WithMessage("انتخاب مالکیت الزامی است.");


        }
    }

    public class PhoneNumberEditValidator : AbstractValidator<PhoneNumberUpdateModel>
    {
        public PhoneNumberEditValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0).WithMessage("شناسه شماره تلفن معتبر نیست.");

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("وارد کردن شماره تلفن الزامی است.")
                .MaximumLength(20).WithMessage("شماره تلفن نمی‌تواند بیشتر از 20 کاراکتر باشد.");

            RuleFor(x => x.Ownership)
                .IsInEnum().WithMessage("انتخاب مالکیت الزامی است.");

            RuleFor(x => x.StudentId)
                .GreaterThan(0).WithMessage("شناسه دانش‌آموز معتبر نیست.");
        }
    }

    public class PhoneNumberDeleteValidator : AbstractValidator<PhoneNumberDeleteModel>
    {
        public PhoneNumberDeleteValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0).WithMessage("شناسه شماره تلفن معتبر نیست.");
        }
    }
}
