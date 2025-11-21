using Example.Service.Handler.Commands.ActivityForm;
using Example.Service.Handler.Queries.ActivityForm;
using Example.Service.Models.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Sediq.Web.Api.Controllers
{
    public class ActivityController : BaseController
    {
        private readonly IMediator _mediator;

        public ActivityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> List()
        {
            var result = await _mediator.Send(new GetAllActivityFormsQuery());
            return View(result.IsSuccess ? result.Data : new List<ActivityFormResponseDto>());
        }

        public async Task<IActionResult> Create()
        {
            var programs = await _mediator.Send(new Example.Service.Handler.Queries.Program.GetAllProgramsQuery());
            var students = await _mediator.Send(new Example.Service.Handler.Queries.Student.GetAllStudentsQuery());
            
            ViewBag.Programs = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(programs.Data ?? new List<ProgramResponseDto>(), "RowId", "Name");
            ViewBag.Students = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(students.Data ?? new List<StudentResponseDto>(), "RowId", "FullName");
            
            return View(new ActivityFormCreateModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ActivityFormCreateModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var command = new CreateActivityFormCommand
            {
                ActivityDate = model.ActivityDate,
                SelectedProgramId = model.SelectedProgramId,
                SelectedStudentId = model.SelectedStudentId
            };
            var result = await _mediator.Send(command);
            
            if (result.IsSuccess)
            {
                TempData["Success"] = "فعالیت با موفقیت ثبت شد";
                return RedirectToAction(nameof(List));
            }
            
            ModelState.AddModelError("", result.ResponseDesc ?? "خطا در ثبت فعالیت");
            return View(model);
        }

        public async Task<IActionResult> Details(long id)
        {
            var result = await _mediator.Send(new GetActivityFormById { Id = (int)id });
            if (result == null)
                return NotFound();
            
            return PartialView("_DetailsPartial", result);
        }

        public async Task<IActionResult> Edit(long id)
        {
            var result = await _mediator.Send(new GetActivityFormById { Id = (int)id });
            if (result == null)
                return NotFound();

            var model = new ActivityFormUpdateModel
            {
                RowId = result.RowId,
                ActivityDate = result.ActivityDate,
                SelectedProgramId = result.SelectedProgramId,
                SelectedStudentId = result.SelectedStudentId
            };
            
            return PartialView("_EditPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ActivityFormUpdateModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "اطلاعات وارد شده معتبر نیست" });

            var result = await _mediator.Send(new UpdateActivityFormCommand(model));
            
            if (result.IsSuccess)
                return Json(new { success = true, message = "فعالیت با موفقیت به روز شد" });
            
            return Json(new { success = false, message = result.ResponseDesc ?? "خطا در به روز رسانی" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _mediator.Send(new DeleteActivityFormCommand { Id = (int)id });
            
            if (result.IsSuccess)
                return Json(new { success = true, message = "فعالیت با موفقیت حذف شد" });
            
            return Json(new { success = false, message = result.ResponseDesc ?? "خطا در حذف" });
        }
    }
}
