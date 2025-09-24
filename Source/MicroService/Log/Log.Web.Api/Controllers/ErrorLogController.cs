using Log.Service.Handler.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Log.Web.Api.Controllers
{

    public class ErrorLogController : BaseController
    {
        private readonly IMediator _mediator;

        public ErrorLogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetLogs([FromQuery] string? serviceName, [FromQuery] string? logType)
        {
            var result = await _mediator.Send(new GetErrorLogsQuery(serviceName, logType));
            return Ok(result);
        }
    }
}

