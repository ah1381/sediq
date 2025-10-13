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
            return View(result.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DurationDateEntityCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new CreateDurationDateCommand
                {
                    Name = model.Name,
                    StartDur = model.StartDur,
                    EndDur = model.EndDur
                };
                var result = await _mediator.Send(command);
                if (result.IsSuccess)
                {
                    TempData["Success"] = "بازه زمانی با موفقیت ایجاد شد";
                    return RedirectToAction("List");
                }
                ModelState.AddModelError("", "خطا در ایجاد بازه زمانی");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetDurationDateById { Id = id });
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] DurationDateEntityUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new UpdateDurationDateCommand(model));
                return Json(new { success = result.IsSuccess, message = result.IsSuccess ? "بازه زمانی با موفقیت ویرایش شد" : "خطا در ویرایش بازه زمانی" });
            }
            return Json(new { success = false, message = "اطلاعات وارد شده معتبر نیست" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteDurationDateCommand { Id = id });
            return Json(new { success = result.IsSuccess, message = result.IsSuccess ? "بازه زمانی با موفقیت حذف شد" : "خطا در حذف بازه زمانی" });
        }

        public async Task<IActionResult> Update(long id)
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

    }
}