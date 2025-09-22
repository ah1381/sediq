using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Service.Handler.Commands.Example;
using FluentValidation;


namespace Example.Web.Api.Validations
{
    public class CreateExampleValidator : AbstractValidator<CreateExampleCommand>
    {
        public CreateExampleValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100)
                .WithMessage("Name must not exceed 100 characters.");
        }
    }
}
