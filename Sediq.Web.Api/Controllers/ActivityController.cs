using Example.Service.Handler.Commands.ActivityForm;
using Example.Service.Handler.Queries.ActivityForm;
using Example.Service.Handler.Queries.Program;
using Example.Service.Handler.Queries.Student;
using Example.Service.Models.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sediq.Web.Api.Controllers
{
    public class ActivityController : BaseController
    {
        private readonly IMediator _mediator;

        public ActivityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Create()
        {
            await LoadSelectLists();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ActivityFormCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new CreateActivityFormCommand
                {
                    ActivityDate = model.ActivityDate,
                    SelectedProgramId = model.SelectedProgramId,
                    SelectedStudentIds = model.SelectedStudentIds
                };
                var result = await _mediator.Send(command);
                if (result.IsSuccess)
                {
                    TempData["Success"] = "فعالیت با موفقیت ایجاد شد";
                    return RedirectToAction("Create");
                }
                ModelState.AddModelError("", "خطا در ایجاد فعالیت");
            }
            await LoadSelectLists();
            return View(model);
        }

        public async Task<IActionResult> List()
        {
            var result = await _mediator.Send(new GetAllActivityFormsQuery());
            return View(result);
        }

        private async Task LoadSelectLists()
        {
            var programs = await _mediator.Send(new GetAllProgramsQuery());
            var students = await _mediator.Send(new GetAllStudentsQuery());

            ViewBag.Programs = new SelectList(programs.Data, "RowId", "Name");
            ViewBag.Students = new SelectList(students.Data, "RowId", "FullName");
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


        public async Task<IActionResult> Update(long id)
        {
            var result = await _mediator.Send(new GetActivityFormById { Id = (int)id });
            if (result == null)
                return NotFound();

            var model = new ActivityFormUpdateModel
            {
                RowId = result.RowId,
                ActivityDate = result.ActivityDate,
                SelectedProgramId = result.SelectedProgramId,
                SelectedStudentIds = result.SelectedStudentIds
            };
            
            return PartialView("_EditPartial", model);
        }

    }
}