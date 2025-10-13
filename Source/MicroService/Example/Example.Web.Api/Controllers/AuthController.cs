using Example.Service.Handler.Commands.Auth;
using Example.Service.Handler.Queries.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raya.Hrm.Shared.Library.Models.Auth;
using System.Security.Claims;

namespace Example.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator) => _mediator = mediator;

        [HttpPost("create-user")]
        [Authorize]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(username))
                    return Unauthorized("User not authenticated");

                var requestModel = new CreateUserInfo()
                {
                    CurrentUser = username,
                    Username = request.Username,
                    Password = request.Password
                };

                var result = await _mediator.Send(new CreateUserCommand() { RequestModel = requestModel });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating user: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestModel request)
        {
            try
            {
                var result = await _mediator.Send(new LoginQuery() { RequestModel = request });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Login failed: {ex.Message}");
            }
        }
    }
}
