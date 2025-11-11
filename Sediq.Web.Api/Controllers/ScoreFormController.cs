using Example.Service.Handler.Commands.ScoreForm;
using Example.Service.Handler.Queries.ScoreForm;
using Example.Service.Models.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
            return View(result.IsSuccess ? result.Data : new List<ScoreFormResponseDto>());
        }

        public IActionResult Create()
        {
            return View(new ScoreFormCreateModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ScoreFormCreateModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

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
                TempData["Success"] = "نمره با موفقیت ثبت شد";
                return RedirectToAction(nameof(List));
            }
            
            ModelState.AddModelError("", result.ResponseDesc ?? "خطا در ثبت نمره");
            return View(model);
        }

        public async Task<IActionResult> Details(long id)
        {
            var result = await _mediator.Send(new GetScoreFormById { Id = (int)id });
            if (result == null)
                return NotFound();
            
            return PartialView("_DetailsPartial", result);
        }

        public async Task<IActionResult> Edit(long id)
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

        [HttpPost]
        public async Task<IActionResult> Edit(ScoreFormUpdateModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "اطلاعات وارد شده معتبر نیست" });

            var result = await _mediator.Send(new UpdateScoreFormCommand(model));
            
            if (result.IsSuccess)
                return Json(new { success = true, message = "نمره با موفقیت به روز شد" });
            
            return Json(new { success = false, message = result.ResponseDesc ?? "خطا در به روز رسانی" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _mediator.Send(new DeleteScoreFormCommand { Id = (int)id });
            
            if (result.IsSuccess)
                return Json(new { success = true, message = "نمره با موفقیت حذف شد" });
            
            return Json(new { success = false, message = result.ResponseDesc ?? "خطا در حذف" });
        }
    }
}
