using Example.Service.Models.DTOs;
using FluentValidation;

namespace Sediq.Web.Api.Validations
{
    public class sediqCreateValidator : AbstractValidator<sediqCreateModel>
    {
        public sediqCreateValidator()
        {
            RuleFor(x => x.sediqCode)
                .GreaterThan(0).WithMessage("کد صدیق باید بزرگتر از صفر باشد");

            RuleFor(x => x.sediqName)
                .NotEmpty().WithMessage("نام صدیق الزامی است")
                .MaximumLength(200).WithMessage("نام صدیق نمیتواند بیش از 200 کاراکتر باشد");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("تاریخ شروع الزامی است");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("توضیحات نمیتواند بیش از 1000 کاراکتر باشد");
        }
    }

    public class sediqUpdateValidator : AbstractValidator<sediqUpdateModel>
    {
        public sediqUpdateValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0).WithMessage("شناسه صدیق معتبر نیست");

            RuleFor(x => x.sediqCode)
                .GreaterThan(0).WithMessage("کد صدیق باید بزرگتر از صفر باشد");

            RuleFor(x => x.sediqName)
                .NotEmpty().WithMessage("نام صدیق الزامی است")
                .MaximumLength(200).WithMessage("نام صدیق نمیتواند بیش از 200 کاراکتر باشد");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("تاریخ شروع الزامی است");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("توضیحات نمیتواند بیش از 1000 کاراکتر باشد");
        }
    }
}