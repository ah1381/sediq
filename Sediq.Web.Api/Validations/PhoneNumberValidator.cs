using Example.Service.Models.DTOs;
using FluentValidation;

namespace Sediq.Web.Api.Validations
{
    public class PhoneNumberCreateValidator : AbstractValidator<PhoneNumberCreateModel>
    {
        public PhoneNumberCreateValidator()
        {
            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("شماره تلفن الزامی است")
                .Matches(@"^09\d{9}$").WithMessage("شماره تلفن باید با 09 شروع شده و 11 رقم باشد");


        }
    }

    public class PhoneNumberUpdateValidator : AbstractValidator<PhoneNumberUpdateModel>
    {
        public PhoneNumberUpdateValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0).WithMessage("شناسه شماره تلفن معتبر نیست");

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("شماره تلفن الزامی است")
                .Matches(@"^09\d{9}$").WithMessage("شماره تلفن باید با 09 شروع شده و 11 رقم باشد");

            RuleFor(x => x.StudentId)
                .GreaterThan(0).WithMessage("شناسه دانشآموز معتبر نیست");
        }
    }
}