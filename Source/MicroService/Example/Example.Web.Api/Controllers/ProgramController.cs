using Example.Service.Handler.Commands.Program;
using Example.Service.Handler.Queries.Program;
using Example.Service.Models.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Example.Web.Api.Controllers
{
    public class ProgramController : BaseController
    {
        private readonly IMediator _mediator;

        public ProgramController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProgramCreateDto command)
        {
            var newId = await _mediator.Send(new CreateProgramCommand { RequestModel = command});
            return Ok(new { Id = newId });
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] ProgramEditDto command)
        {
            var newId = await _mediator.Send(new EditProgramCommand { RequestModel = command });
            return Ok(new { Id = newId });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetProgramQuery { Id = id };
            var result = await _mediator.Send(query);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteProgramCommand { RequestModel = new ProgramDeleteDto { Id = id } };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
