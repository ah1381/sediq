using Example.Service.Handler.Commands.ActivityForm;
using Example.Service.Handler.Queries.ActivityForm;
using Example.Service.Models.DTOs;
using Example.Web.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Example.WebApi.Controllers
{
    public class ActivityController : BaseController
    {
        private readonly IMediator _mediator;

        public ActivityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ActivityFormCreateDto command)
        {
            var newId = await _mediator.Send(new CreateActivityFormCommand { RequestModel = command });
            return Ok(new { Id = newId });
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] ActivityFormEditDto command)
        {
            var newId = await _mediator.Send(new EditActivityFormCommand { RequestModel = command });
            return Ok(new { Id = newId });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetActivityFormQuery { Id = id };
            var result = await _mediator.Send(query);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteActivityFormCommand { RequestModel = new ActivityFormDeleteDto { Id = id } };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetAllActivityFormsQuery { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}