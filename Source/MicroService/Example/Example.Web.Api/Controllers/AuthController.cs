using Example.Service.Handler.Commands.Auth;
using Example.Service.Handler.Queries.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raya.Hrm.Shared.Library.Models.Auth;
using System.Security.Claims;
using ZstdSharp;

namespace Example.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
        {
            var a = User.Claims.Select(x => x.Value == "Admin");

            // var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
            //throw new Exception($"Custom error triggered {request.Username}!");

            var username = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var requestModel = new CreateUserInfo()
            {
                CurrentUser = username,
                Username = request.Username,
                Password = request.Password
            };

            return Ok(await _mediator.Send(new CreateUserCommand() { RequestModel = requestModel }));
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginRequestModel request) =>
            Ok(await _mediator.Send(new LoginQuery() { RequestModel = request }));
    }
}
