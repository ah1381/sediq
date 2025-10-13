using Example.Service.Models.DTOs;
using FluentValidation;

namespace Example.Web.Api.Validations
{
    public class ScoreFormCreateValidator : AbstractValidator<ScoreFormCreateModel>
    {
        public ScoreFormCreateValidator()
        {
            RuleFor(x => x.SelectedProgramId)
                .GreaterThan(0).WithMessage("شناسه برنامه انتخابی معتبر نیست.");

            RuleFor(x => x.SelectedStudentId)
                .GreaterThan(0).WithMessage("شناسه دانشآموز انتخابی معتبر نیست.");

            RuleFor(x => x.Score)
                .GreaterThanOrEqualTo(0).WithMessage("نمره نمیتواند منفی باشد.")
                .LessThanOrEqualTo(100).WithMessage("نمره نمیتواند بیش از 100 باشد.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("توضیحات نمیتواند بیش از 500 کاراکتر باشد.");

            RuleFor(x => x.ActivityDurId)
                .GreaterThan(0).WithMessage("شناسه بازه فعالیت معتبر نیست.");
        }
    }

    public class ScoreFormUpdateValidator : AbstractValidator<ScoreFormUpdateModel>
    {
        public ScoreFormUpdateValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0).WithMessage("شناسه فرم نمره معتبر نیست.");

            RuleFor(x => x.SelectedProgramId)
                .GreaterThan(0).WithMessage("شناسه برنامه انتخابی معتبر نیست.");

            RuleFor(x => x.SelectedStudentId)
                .GreaterThan(0).WithMessage("شناسه دانشآموز انتخابی معتبر نیست.");

            RuleFor(x => x.Score)
                .GreaterThanOrEqualTo(0).WithMessage("نمره نمیتواند منفی باشد.")
                .LessThanOrEqualTo(100).WithMessage("نمره نمیتواند بیش از 100 باشد.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("توضیحات نمیتواند بیش از 500 کاراکتر باشد.");

            RuleFor(x => x.ActivityDurId)
                .GreaterThan(0).WithMessage("شناسه بازه فعالیت معتبر نیست.");
        }
    }
}