using Example.Service.Handler.Commands.ScoreForm;
using Example.Service.Handler.Queries.ScoreForm;
using Example.Service.Handler.Queries.Program;
using Example.Service.Handler.Queries.Student;
using Example.Service.Handler.Queries.DurationDate;
using Example.Service.Models.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sediq.Web.Api.Controllers
{
    public class ScoreFormController : BaseController
    {
        private readonly IMediator _mediator;

        public ScoreFormController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> List()
        {
            var result = await _mediator.Send(new GetAllScoreFormsQuery());
            return View(result.Data);
        }

        public async Task<IActionResult> Create()
        {
            await LoadSelectLists();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ScoreFormCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new CreateScoreFormCommand
                {
                    SelectedProgramId = model.SelectedProgramId,
                    SelectedStudentId = model.SelectedStudentId,
                    Description = model.Description,
                    Score = model.Score,
                    ActivityDurId = model.ActivityDurId
                };
                var result = await _mediator.Send(command);
                if (result.IsSuccess)
                {
                    TempData["Success"] = "فرم نمره با موفقیت ایجاد شد";
                    return RedirectToAction("List");
                }
                ModelState.AddModelError("", "خطا در ایجاد فرم نمره");
            }
            await LoadSelectLists();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetScoreFormById { Id = id });
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] ScoreFormUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new UpdateScoreFormCommand(model));
                return Json(new { success = result.IsSuccess, message = result.IsSuccess ? "فرم نمره با موفقیت ویرایش شد" : "خطا در ویرایش فرم نمره" });
            }
            return Json(new { success = false, message = "اطلاعات وارد شده معتبر نیست" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteScoreFormCommand { Id = id });
            return Json(new { success = result.IsSuccess, message = result.IsSuccess ? "فرم نمره با موفقیت حذف شد" : "خطا در حذف فرم نمره" });
        }

        public async Task<IActionResult> Update(long id)
        {
            var result = await _mediator.Send(new GetScoreFormById { Id = (int)id });
            if (result == null)
                return NotFound();

            var model = new ScoreFormUpdateModel
            {
                RowId = result.RowId,
                SelectedProgramId = result.SelectedProgramId,
                SelectedStudentId = result.SelectedStudentId,
                Description = result.Description,
                Score = result.Score,
                ActivityDurId = result.ActivityDurId
            };
            
            return PartialView("_EditPartial", model);
        }


        private async Task LoadSelectLists()
        {
            var programs = await _mediator.Send(new GetAllProgramsQuery());
            var students = await _mediator.Send(new GetAllStudentsQuery());
            var durations = await _mediator.Send(new GetAllDurationDatesQuery());

            ViewBag.Programs = new SelectList(programs.Data, "RowId", "Name");
            ViewBag.Students = new SelectList(students.Data, "RowId", "FullName");
            ViewBag.Durations = new SelectList(durations.Data, "RowId", "Name");
        }
    }
}