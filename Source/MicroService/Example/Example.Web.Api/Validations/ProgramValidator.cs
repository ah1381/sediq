using Example.Service.Models.DTOs;
using FluentValidation;

namespace Example.Web.Api.Validations
{
    public class ProgramCreateValidator : AbstractValidator<ProgramCreateModel>
    {
        public ProgramCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("وارد کردن نام برنامه الزامی است.")
                .MaximumLength(100).WithMessage("نام برنامه نمی‌تواند بیش از 100 کاراکتر باشد.");

            RuleFor(x => x.From)
                .LessThanOrEqualTo(x => x.To).WithMessage("تاریخ شروع نمی‌تواند بعد از تاریخ پایان باشد.");

            RuleFor(x => x.To)
                .GreaterThanOrEqualTo(x => x.From).WithMessage("تاریخ پایان نمی‌تواند قبل از تاریخ شروع باشد.");
        }
    }

    public class ProgramEditValidator : AbstractValidator<ProgramUpdateModel>
    {
        public ProgramEditValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0).WithMessage("شناسه برنامه معتبر نیست.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("وارد کردن نام برنامه الزامی است.")
                .MaximumLength(100).WithMessage("نام برنامه نمی‌تواند بیش از 100 کاراکتر باشد.");

            RuleFor(x => x.From)
                .LessThanOrEqualTo(x => x.To).WithMessage("تاریخ شروع نمی‌تواند بعد از تاریخ پایان باشد.");

            RuleFor(x => x.To)
                .GreaterThanOrEqualTo(x => x.From).WithMessage("تاریخ پایان نمی‌تواند قبل از تاریخ شروع باشد.");
        }
    }

    public class ProgramDeleteValidator : AbstractValidator<ProgramDeleteModel>
    {
        public ProgramDeleteValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0).WithMessage("شناسه برنامه معتبر نیست.");
        }
    }
}
