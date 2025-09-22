using MediatR;
using Microsoft.Extensions.Options;
using Raya.Hrm.Shared.Library.GenaralAuthService;
using Raya.Hrm.Shared.Library.Models.Auth;
using Raya.Hrm.Shared.Library.Models.Base;
using Raya.Hrm.Shared.Library.Utilities.Securities;

namespace LoanManagement.Service.Handler.Queries.Auth
{
    public class LoginQuery : IRequest<CustomActionResult<LoginResponseModel?>>
    {
        public LoginRequestModel RequestModel { get; set; }
    }

    public class LoginQueryHandler(IAuthService repo, IOptions<JwtConfig> config)
    : IRequestHandler<LoginQuery, CustomActionResult<LoginResponseModel?>>
    {
        public async Task<CustomActionResult<LoginResponseModel?>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var res = new CustomActionResult<LoginResponseModel?>
            {
                IsSuccess = false,
                ResponseType = -2
            };
            var userinfo = await repo.GetUserForLogin(request.RequestModel.Username);
            if (userinfo == null)
            {

                res.ResponseDesc = "Invalid Username";
                return res;
            }

            var token = Security.GenerateJSONWebToken(request.RequestModel, config.Value, userinfo.UserType);
            res.Data = new LoginResponseModel()
            {
                Token = token
            };

            res.IsSuccess = true;
            res.ResponseType = 0;
            return res;
        }
    }
}
