using Example.Service.Models.DTOs;
using FluentValidation;

namespace Sediq.Web.Api.Validations
{
    public class DurationDateCreateValidator : AbstractValidator<DurationDateEntityCreateModel>
    {
        public DurationDateCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("نام بازه زمانی الزامی است")
                .MaximumLength(200).WithMessage("نام بازه زمانی نمیتواند بیش از 200 کاراکتر باشد");

            RuleFor(x => x.StartDur)
                .NotEmpty().WithMessage("تاریخ شروع الزامی است")
                .LessThanOrEqualTo(x => x.EndDur).WithMessage("تاریخ شروع نمیتواند بعد از تاریخ پایان باشد");

            RuleFor(x => x.EndDur)
                .NotEmpty().WithMessage("تاریخ پایان الزامی است")
                .GreaterThanOrEqualTo(x => x.StartDur).WithMessage("تاریخ پایان نمیتواند قبل از تاریخ شروع باشد");
        }
    }

    public class DurationDateUpdateValidator : AbstractValidator<DurationDateEntityUpdateModel>
    {
        public DurationDateUpdateValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0).WithMessage("شناسه بازه زمانی معتبر نیست");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("نام بازه زمانی الزامی است")
                .MaximumLength(200).WithMessage("نام بازه زمانی نمیتواند بیش از 200 کاراکتر باشد");

            RuleFor(x => x.StartDur)
                .NotEmpty().WithMessage("تاریخ شروع الزامی است")
                .LessThanOrEqualTo(x => x.EndDur).WithMessage("تاریخ شروع نمیتواند بعد از تاریخ پایان باشد");

            RuleFor(x => x.EndDur)
                .NotEmpty().WithMessage("تاریخ پایان الزامی است")
                .GreaterThanOrEqualTo(x => x.StartDur).WithMessage("تاریخ پایان نمیتواند قبل از تاریخ شروع باشد");
        }
    }
}