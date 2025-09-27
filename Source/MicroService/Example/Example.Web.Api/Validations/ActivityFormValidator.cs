using Example.Service.Models.DTOs;
using FluentValidation;

namespace Example.Web.Api.Validations
{
    public class ActivityFormCreateValidator : AbstractValidator<ActivityFormCreateDto>
    {
        public ActivityFormCreateValidator()
        {
            RuleFor(x => x.ActivityDate)
                .NotEmpty().WithMessage("تاریخ انجام فعالیت الزامی است.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("تاریخ انجام فعالیت نمی‌تواند در آینده باشد.");

            RuleFor(x => x.SelectedProgramId)
                .GreaterThan(0).WithMessage("شناسه برنامه انتخابی معتبر نیست.");

            RuleFor(x => x.SelectedStudentIds)
                .NotEmpty().WithMessage("حداقل یک دانش‌آموز باید انتخاب شود.");
        }
    }

    public class ActivityFormEditValidator : AbstractValidator<ActivityFormEditDto>
    {
        public ActivityFormEditValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("شناسه فرم معتبر نیست.");

            RuleFor(x => x.ActivityDate)
                .NotEmpty().WithMessage("تاریخ انجام فعالیت الزامی است.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("تاریخ انجام فعالیت نمی‌تواند در آینده باشد.");

            RuleFor(x => x.SelectedProgramId)
                .GreaterThan(0).WithMessage("شناسه برنامه انتخابی معتبر نیست.");

            RuleFor(x => x.SelectedStudentIds)
                .NotEmpty().WithMessage("حداقل یک دانش‌آموز باید انتخاب شود.");
        }
    }

    public class ActivityFormDeleteValidator : AbstractValidator<ActivityFormDeleteDto>
    {
        public ActivityFormDeleteValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("شناسه فرم معتبر نیست.");
        }
    }
}
