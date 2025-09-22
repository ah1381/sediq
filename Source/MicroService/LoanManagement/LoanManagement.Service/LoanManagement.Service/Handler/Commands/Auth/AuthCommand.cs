using MediatR;
using Raya.Hrm.Shared.Library.GenaralAuthService;
using Raya.Hrm.Shared.Library.Models.Auth;
using Raya.Hrm.Shared.Library.Models.Base;
using Raya.Hrm.Shared.Library.Utilities.Securities;

namespace LoanManagement.Service.Handler.Commands.Auth
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
