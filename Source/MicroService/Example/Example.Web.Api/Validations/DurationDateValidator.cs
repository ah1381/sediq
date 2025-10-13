using Example.Service.Models.DTOs;
using FluentValidation;

namespace Example.Web.Api.Validations
{
    public class DurationDateCreateValidator : AbstractValidator<DurationDateEntityCreateModel>
    {
        public DurationDateCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("نام بازه زمانی الزامی است.")
                .MaximumLength(100).WithMessage("نام بازه زمانی نمیتواند بیش از 100 کاراکتر باشد.");

            RuleFor(x => x.StartDur)
                .LessThanOrEqualTo(x => x.EndDur).WithMessage("تاریخ شروع نمیتواند بعد از تاریخ پایان باشد.");

            RuleFor(x => x.EndDur)
                .GreaterThanOrEqualTo(x => x.StartDur).WithMessage("تاریخ پایان نمیتواند قبل از تاریخ شروع باشد.");
        }
    }

    public class DurationDateUpdateValidator : AbstractValidator<DurationDateEntityUpdateModel>
    {
        public DurationDateUpdateValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0).WithMessage("شناسه بازه زمانی معتبر نیست.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("نام بازه زمانی الزامی است.")
                .MaximumLength(100).WithMessage("نام بازه زمانی نمیتواند بیش از 100 کاراکتر باشد.");

            RuleFor(x => x.StartDur)
                .LessThanOrEqualTo(x => x.EndDur).WithMessage("تاریخ شروع نمیتواند بعد از تاریخ پایان باشد.");

            RuleFor(x => x.EndDur)
                .GreaterThanOrEqualTo(x => x.StartDur).WithMessage("تاریخ پایان نمیتواند قبل از تاریخ شروع باشد.");
        }
    }
}