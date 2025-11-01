using Example.Service.Models.DTOs;
using FluentValidation;

namespace Sediq.Web.Api.Validations
{
    public class ProgramCreateValidator : AbstractValidator<ProgramCreateModel>
    {
        public ProgramCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("نام برنامه الزامی است")
                .MaximumLength(200).WithMessage("نام برنامه نمیتواند بیش از 200 کاراکتر باشد");

            RuleFor(x => x.From)
                .LessThanOrEqualTo(x => x.To).When(x => x.From.HasValue && x.To.HasValue)
                .WithMessage("تاریخ شروع نمیتواند بعد از تاریخ پایان باشد");

            RuleFor(x => x.To)
                .GreaterThanOrEqualTo(x => x.From).When(x => x.From.HasValue && x.To.HasValue)
                .WithMessage("تاریخ پایان نمیتواند قبل از تاریخ شروع باشد");
        }
    }

    public class ProgramUpdateValidator : AbstractValidator<ProgramUpdateModel>
    {
        public ProgramUpdateValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0).WithMessage("شناسه برنامه معتبر نیست");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("نام برنامه الزامی است")
                .MaximumLength(200).WithMessage("نام برنامه نمیتواند بیش از 200 کاراکتر باشد");

            RuleFor(x => x.From)
                .LessThanOrEqualTo(x => x.To).When(x => x.From.HasValue && x.To.HasValue)
                .WithMessage("تاریخ شروع نمیتواند بعد از تاریخ پایان باشد");

            RuleFor(x => x.To)
                .GreaterThanOrEqualTo(x => x.From).When(x => x.From.HasValue && x.To.HasValue)
                .WithMessage("تاریخ پایان نمیتواند قبل از تاریخ شروع باشد");
        }
    }
}