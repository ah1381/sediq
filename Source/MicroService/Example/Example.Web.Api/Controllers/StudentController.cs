using Example.Service.Handler.Commands.Student;
using Example.Service.Handler.Queries.Student;
using Example.Service.Models.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Example.Web.Api.Controllers
{
    public class StudentController : BaseController
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentCreateDto command)
        {
            var newId = await _mediator.Send(new CreateStudentCommand { RequestModel = command});
            return Ok(new { Id = newId });
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] StudentEditDto command)
        {
            var newId = await _mediator.Send(new EditStudentCommand { RequestModel = command });
            return Ok(new { Id = newId });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetStudentQuery { Id = id };
            var result = await _mediator.Send(query);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteStudentCommand { RequestModel = new StudentDeleteDto { Id = id } };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
