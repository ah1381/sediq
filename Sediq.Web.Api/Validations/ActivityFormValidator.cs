using Example.Service.Models.DTOs;
using FluentValidation;

namespace Sediq.Web.Api.Validations
{
    public class ActivityFormCreateValidator : AbstractValidator<ActivityFormCreateModel>
    {
        public ActivityFormCreateValidator()
        {
            RuleFor(x => x.ActivityDate)
                .NotEmpty().WithMessage("تاریخ فعالیت الزامی است")
                .Must(date => date <= DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("تاریخ فعالیت نمیتواند در آینده باشد");

            RuleFor(x => x.SelectedProgramId)
                .GreaterThan(0).WithMessage("انتخاب برنامه الزامی است");

            RuleFor(x => x.SelectedStudentIds)
                .NotEmpty().WithMessage("انتخاب حداقل یک دانشآموز الزامی است")
                .Must(list => list != null && list.Any())
                .WithMessage("لیست دانشآموزان نمیتواند خالی باشد");
        }
    }

    public class ActivityFormUpdateValidator : AbstractValidator<ActivityFormUpdateModel>
    {
        public ActivityFormUpdateValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0).WithMessage("شناسه فعالیت معتبر نیست");

            RuleFor(x => x.ActivityDate)
                .NotEmpty().WithMessage("تاریخ فعالیت الزامی است")
                .Must(date => date <= DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("تاریخ فعالیت نمیتواند در آینده باشد");

            RuleFor(x => x.SelectedProgramId)
                .GreaterThan(0).WithMessage("انتخاب برنامه الزامی است");

            RuleFor(x => x.SelectedStudentIds)
                .NotEmpty().WithMessage("انتخاب حداقل یک دانشآموز الزامی است")
                .Must(list => list != null && list.Any())
                .WithMessage("لیست دانشآموزان نمیتواند خالی باشد");
        }
    }
}