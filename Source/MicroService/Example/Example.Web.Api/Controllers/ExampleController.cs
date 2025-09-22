using Example.Service.Handler.Commands.Example;
using Example.Service.Handler.Queries.Example;
using Example.Web.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Example.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExampleController : BaseController
    {
        private readonly IMediator _mediator;

        public ExampleController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateExampleCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newId = await _mediator.Send(command);
            return Ok(new { Id = newId });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetExampleQuery { Id = id };
            var result = await _mediator.Send(query);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteExampleCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}