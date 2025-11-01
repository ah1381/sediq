using Example.Service.Models.DTOs;
using FluentValidation;

namespace Sediq.Web.Api.Validations
{
    public class ImagesCreateValidator : AbstractValidator<ImagesCreateModel>
    {
        public ImagesCreateValidator()
        {
            RuleFor(x => x.PhotoUrl)
                .NotEmpty().WithMessage("آدرس عکس الزامی است")
                .MaximumLength(500).WithMessage("آدرس عکس نمیتواند بیش از 500 کاراکتر باشد");

            RuleFor(x => x.Description)
                .MaximumLength(200).WithMessage("توضیحات عکس نمیتواند بیش از 200 کاراکتر باشد");

        }
    }

    public class ImagesUpdateValidator : AbstractValidator<ImagesUpdateModel>
    {
        public ImagesUpdateValidator()
        {
            RuleFor(x => x.RowId)
                .GreaterThan(0).WithMessage("شناسه عکس معتبر نیست");

            RuleFor(x => x.PhotoUrl)
                .NotEmpty().WithMessage("آدرس عکس الزامی است")
                .MaximumLength(500).WithMessage("آدرس عکس نمیتواند بیش از 500 کاراکتر باشد");

            RuleFor(x => x.Description)
                .MaximumLength(200).WithMessage("توضیحات عکس نمیتواند بیش از 200 کاراکتر باشد");

            RuleFor(x => x.StudentId)
                .GreaterThan(0).WithMessage("شناسه دانش آموز معتبر نیست");
        }
    }
}