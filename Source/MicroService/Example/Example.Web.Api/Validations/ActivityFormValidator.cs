using Example.Service.Models.DTOs;
using FluentValidation;

namespace Example.Web.Api.Validations
{
    public class ActivityFormCreateValidator : AbstractValidator<ActivityFormCreateModel>
    {
        public ActivityFormCreateValidator()
        {
            RuleFor(x => x.ActivityDate)
                .NotEmpty().WithMessage("تاریخ انجام فعالیت الزامی است.")
                .Must(date => date <= DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("تاریخ انجام فعالیت نمیتواند در آینده باشد.");

            RuleFor(x => x.SelectedProgramId)
                .GreaterThan(0).WithMessage("شناسه برنامه انتخابی معتبر نیست.");

            RuleFor(x => x.SelectedStudentIds)
                .NotEmpty().WithMessage("حداقل یک دانش آموز باید انتخاب شود.");
        }
    }

    public class ActivityFormEditValidator : AbstractValidator<ActivityFormUpdateModel>
    {
        public ActivityFormEditValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0).WithMessage("شناسه فرم معتبر نیست.");

            RuleFor(x => x.ActivityDate)
                .Must(date => date <= DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("تاریخ انجام فعالیت نمیتواند در آینده باشد.");

            RuleFor(x => x.SelectedProgramId)
                .GreaterThan(0).WithMessage("شناسه برنامه انتخابی معتبر نیست.");

            RuleFor(x => x.SelectedStudentIds)
                .NotEmpty().WithMessage("حداقل یک دانش آموز باید انتخاب شود.");
        }
    }

    public class ActivityFormDeleteValidator : AbstractValidator<ActivityFormDeleteModel>
    {
        public ActivityFormDeleteValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0).WithMessage("شناسه فرم معتبر نیست.");
        }
    }
}