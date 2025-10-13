using Example.Service.Handler.Commands.ActivityForm;
using Example.Service.Handler.Queries.ActivityForm;
using Example.Service.Models.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Example.Web.Api.Controllers
{
    public class ActivityController : BaseController
    {
        private readonly IMediator _mediator;

        public ActivityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetActivityFormById { Id = id });
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllActivityFormsQuery());
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ActivityFormUpdateModel dto)
        {
            dto.RowId = id;
            var result = await _mediator.Send(new UpdateActivityFormCommand(dto));
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteActivityFormCommand { Id = id });
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}