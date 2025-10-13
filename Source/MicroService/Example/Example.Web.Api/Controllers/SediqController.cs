using Example.Service.Handler.Commands.sediq;
using Example.Service.Handler.Queries.sediq;
using Example.Service.Models.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Example.Web.Api.Controllers;



public class SediqController : BaseController
{
    private readonly IMediator _mediator;

    public SediqController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _mediator.Send(new GetsediqById { Id = id });
        return result != null ? Ok(result) : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllsediqsQuery());
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] sediqCreateModel dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new CreatesediqCommand
            {
                sediqCode = dto.sediqCode,
                sediqName = dto.sediqName,
                StartDate = dto.StartDate,
                Description = dto.Description
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
    public async Task<IActionResult> Update(int id, [FromBody] sediqUpdateModel dto)
    {
        dto.RowId = id;
        var result = await _mediator.Send(new UpdatesediqCommand(dto));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeletesediqCommand { Id = id });
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}