using Example.Service.Handler.Commands.DurationDate;
using Example.Service.Handler.Queries.DurationDate;
using Example.Service.Models.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Sediq.Web.Api.Controllers
{
    public class DurationDateController : BaseController
    {
        private readonly IMediator _mediator;

        public DurationDateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> List()
        {
            var result = await _mediator.Send(new GetAllDurationDatesQuery());
            return View(result.IsSuccess ? result.Data : new List<DurationDateEntityResponseDto>());
        }

        public IActionResult Create()
        {
            return View(new DurationDateEntityCreateModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DurationDateEntityCreateModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var command = new CreateDurationDateCommand
            {
                Name = model.Name,
                StartDur = model.StartDur,
                EndDur = model.EndDur
            };
            var result = await _mediator.Send(command);
            
            if (result.IsSuccess)
            {
                TempData["Success"] = "بازه زمانی با موفقیت ثبت شد";
                return RedirectToAction(nameof(List));
            }
            
            ModelState.AddModelError("", result.ResponseDesc ?? "خطا در ثبت بازه زمانی");
            return View(model);
        }

        public async Task<IActionResult> Details(long id)
        {
            var result = await _mediator.Send(new GetDurationDateById { Id = (int)id });
            if (result == null)
                return NotFound();
            
            return PartialView("_DetailsPartial", result);
        }

        public async Task<IActionResult> Edit(long id)
        {
            var result = await _mediator.Send(new GetDurationDateById { Id = (int)id });
            if (result == null)
                return NotFound();

            var model = new DurationDateEntityUpdateModel
            {
                RowId = result.RowId,
                Name = result.Name,
                StartDur = result.StartDur,
                EndDur = result.EndDur
            };
            
            return PartialView("_EditPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DurationDateEntityUpdateModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "اطلاعات وارد شده معتبر نیست" });

            var result = await _mediator.Send(new UpdateDurationDateCommand(model));
            
            if (result.IsSuccess)
                return Json(new { success = true, message = "بازه زمانی با موفقیت به روز شد" });
            
            return Json(new { success = false, message = result.ResponseDesc ?? "خطا در به روز رسانی" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _mediator.Send(new DeleteDurationDateCommand { Id = (int)id });
            
            if (result.IsSuccess)
                return Json(new { success = true, message = "بازه زمانی با موفقیت حذف شد" });
            
            return Json(new { success = false, message = result.ResponseDesc ?? "خطا در حذف" });
        }
    }
}
