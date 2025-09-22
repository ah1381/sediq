using Example.Service.Handler.Queries.Example;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Web.Api.Validations
{
    public class GetExampleValidator : AbstractValidator<GetExampleQuery>
    {
        public GetExampleValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0.");
        }
    }
}
