using Example.Service.Models.DTOs;
using FluentValidation;

namespace Sediq.Web.Api.Validations
{
    public class StudentCreateValidator : AbstractValidator<StudentCreateModel>
    {
        public StudentCreateValidator()
        {

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("نام الزامی است")
                .MaximumLength(100).WithMessage("نام نمیتواند بیش از 100 کاراکتر باشد");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("نام خانوادگی الزامی است")
                .MaximumLength(100).WithMessage("نام خانوادگی نمیتواند بیش از 100 کاراکتر باشد");

            RuleFor(x => x.NationalCode)
                .NotEmpty().WithMessage("کد ملی الزامی است")
                .Length(10).WithMessage("کد ملی باید 10 رقم باشد")
                .Matches(@"^\d{10}$").WithMessage("کد ملی باید شامل اعداد باشد");

            RuleFor(x => x.FatherName)
                .MaximumLength(100).WithMessage("نام پدر نمیتواند بیش از 100 کاراکتر باشد");

            RuleFor(x => x.FatherJob)
                .MaximumLength(100).WithMessage("شغل پدر نمیتواند بیش از 100 کاراکتر باشد");

            RuleFor(x => x.FieldOfStudy)
                .MaximumLength(100).WithMessage("رشته تحصیلی نمیتواند بیش از 100 کاراکتر باشد");

            RuleFor(x => x.YearStudy)
                .MaximumLength(50).WithMessage("پایه تحصیلی نمیتواند بیش از 50 کاراکتر باشد");

            RuleFor(x => x.Address)
                .MaximumLength(500).WithMessage("آدرس نمیتواند بیش از 500 کاراکتر باشد");

            RuleFor(x => x.EducationStatus)
                .MaximumLength(100).WithMessage("وضعیت تحصیلی نمیتواند بیش از 100 کاراکتر باشد");

            RuleFor(x => x.Notes)
                .MaximumLength(1000).WithMessage("توضیحات نمیتواند بیش از 1000 کاراکتر باشد");
        }
    }

    public class StudentUpdateValidator : AbstractValidator<StudentUpdateModel>
    {
        public StudentUpdateValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0).WithMessage("شناسه دانشآموز معتبر نیست");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("نام الزامی است")
                .MaximumLength(100).WithMessage("نام نمیتواند بیش از 100 کاراکتر باشد");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("نام خانوادگی الزامی است")
                .MaximumLength(100).WithMessage("نام خانوادگی نمیتواند بیش از 100 کاراکتر باشد");

            RuleFor(x => x.NationalCode)
                .NotEmpty().WithMessage("کد ملی الزامی است")
                .Length(10).WithMessage("کد ملی باید 10 رقم باشد")
                .Matches(@"^\d{10}$").WithMessage("کد ملی باید شامل اعداد باشد");

            RuleFor(x => x.FatherName)
                .MaximumLength(100).WithMessage("نام پدر نمیتواند بیش از 100 کاراکتر باشد");

            RuleFor(x => x.FatherJob)
                .MaximumLength(100).WithMessage("شغل پدر نمیتواند بیش از 100 کاراکتر باشد");

            RuleFor(x => x.FieldOfStudy)
                .MaximumLength(100).WithMessage("رشته تحصیلی نمیتواند بیش از 100 کاراکتر باشد");

            RuleFor(x => x.YearStudy)
                .MaximumLength(50).WithMessage("پایه تحصیلی نمیتواند بیش از 50 کاراکتر باشد");

            RuleFor(x => x.Address)
                .MaximumLength(500).WithMessage("آدرس نمیتواند بیش از 500 کاراکتر باشد");

            RuleFor(x => x.EducationStatus)
                .MaximumLength(100).WithMessage("وضعیت تحصیلی نمیتواند بیش از 100 کاراکتر باشد");

            RuleFor(x => x.Notes)
                .MaximumLength(1000).WithMessage("توضیحات نمیتواند بیش از 1000 کاراکتر باشد");
        }
    }
}