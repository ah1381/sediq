using MediatR;
using Raya.Hrm.Shared.Library.GenaralAuthService;
using Raya.Hrm.Shared.Library.Models.Auth;
using Raya.Hrm.Shared.Library.Models.Base;
using Raya.Hrm.Shared.Library.Utilities.Securities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Service.Handler.Commands.Auth
{
    public class CreateUserCommand : IRequest<CustomActionResult<int>>
    {
        public CreateUserInfo RequestModel { get; set; }
    }

    public class CreateUserCommandHandler(IAuthService repo)
    : IRequestHandler<CreateUserCommand, CustomActionResult<int>>
    {
        public async Task<CustomActionResult<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var res = new CustomActionResult<int>
            {
                IsSuccess = false,
                ResponseType = -2,

            };

            //if (!await validator.IsAdminUser(request.RequestModel))
            //{
            //    res.ResponseDesc = " you are not allow to create user!";
            //    return res;
            //}

            //if (!await validator.UsernameIsNotRepeated(request.RequestModel) ||
            //    request.RequestModel.Username.ToUpper() == "ADMIN")
            //{
            //    res.ResponseDesc = " username defined before";
            //    return res;
            //}

            var hashPass = Security.Encrypt(request.RequestModel.Password);
            request.RequestModel.Password = hashPass;

            var result = await repo.StoreUserAsync(request.RequestModel);
            res.IsSuccess = true;
            res.ResponseType = 0;
            res.Data = result;
            return res;
        }
    }
}
