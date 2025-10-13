using Example.Service.Handler.Commands.Program;
using Example.Service.Handler.Queries.Program;
using Example.Service.Models.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sediq.Web.Api.Controllers;

namespace Sediq.Web.Api.Controllers
{

    public class ProgramController : BaseController
    {
        private readonly IMediator _mediator;

        public ProgramController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetProgramById { Id = id });
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllProgramsQuery());
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProgramCreateModel dto)
        {
            var command = new CreateProgramCommand
            {
                Name = dto.Name,
                From = dto.From,
                To = dto.To
            };
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteProgramCommand { Id = id });
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        public async Task<IActionResult> List()
        {
            var result = await _mediator.Send(new GetAllProgramsQuery());
            return View(result.IsSuccess ? result.Data : new List<ProgramResponseDto>());
        }

        public async Task<IActionResult> Edit(long id)
        {
            var result = await _mediator.Send(new GetProgramById { Id = (int)id });
            if (result == null)
                return NotFound();

            var model = new ProgramUpdateModel
            {
                RowId = result.RowId,
                Name = result.Name,
                From = result.From,
                To = result.To
            };
            
            return PartialView("_EditPartial", model);
        }


    }
}


