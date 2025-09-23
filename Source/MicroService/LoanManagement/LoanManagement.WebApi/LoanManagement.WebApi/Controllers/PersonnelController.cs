using LoanManagement.Service.Handler.Commands.Personnel;
using LoanManagement.Service.Handler.Queries.Personnel;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelController : BaseController
    {
        private readonly IMediator _mediator;

        public PersonnelController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePersonnelCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newId = await _mediator.Send(command);
            return Ok(new { Id = newId });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var query = new GetPersonnelByIdQuery { Id = id };  // Now works with parameterless constructor
            var result = await _mediator.Send(query);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var command = new DeletePersonnelCommand { RowId = id };  // Set RowId instead of Id
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("{id}")]  // Optional: Add if you want to support Update via PUT
        public async Task<IActionResult> Update(long id, [FromBody] UpdatePersonnelCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            command.Personnel.RowId = id;  // Ensure ID is set
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}