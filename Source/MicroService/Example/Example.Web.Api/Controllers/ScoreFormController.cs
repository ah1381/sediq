using Example.Service.Handler.Commands.ScoreForm;
using Example.Service.Handler.Queries.ScoreForm;
using Example.Service.Models.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Example.Web.Api.Controllers;

public class ScoreFormController : BaseController
{
    private readonly IMediator _mediator;

    public ScoreFormController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _mediator.Send(new GetScoreFormById { Id = id });
        return result != null ? Ok(result) : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllScoreFormsQuery());
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ScoreFormCreateModel dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new CreateScoreFormCommand
            {
                SelectedProgramId = dto.SelectedProgramId,
                SelectedStudentId = dto.SelectedStudentId,
                Description = dto.Description,
                Score = dto.Score,
                ActivityDurId = dto.ActivityDurId
            };
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ScoreFormUpdateModel dto)
    {
        dto.RowId = id;
        var result = await _mediator.Send(new UpdateScoreFormCommand(dto));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteScoreFormCommand { Id = id });
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}