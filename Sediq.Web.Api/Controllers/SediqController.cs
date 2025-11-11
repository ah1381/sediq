using Example.Service.Handler.Commands.sediq;
using Example.Service.Handler.Queries.sediq;
using Example.Service.Models.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Sediq.Web.Api.Controllers
{
    public class SediqController : BaseController
    {
        private readonly IMediator _mediator;

        public SediqController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> List()
        {
            var result = await _mediator.Send(new GetAllsediqsQuery());
            return View(result.IsSuccess ? result.Data : new List<sediqResponseDto>());
        }

        public IActionResult Create()
        {
            return View(new sediqCreateModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(sediqCreateModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var command = new CreatesediqCommand
            {
                sediqCode = model.sediqCode,
                sediqName = model.sediqName,
                StartDate = model.StartDate,
                Description = model.Description
            };
            var result = await _mediator.Send(command);
            
            if (result.IsSuccess)
            {
                TempData["Success"] = "صدیق با موفقیت ثبت شد";
                return RedirectToAction(nameof(List));
            }
            
            ModelState.AddModelError("", result.ResponseDesc ?? "خطا در ثبت صدیق");
            return View(model);
        }

        public async Task<IActionResult> Details(long id)
        {
            var result = await _mediator.Send(new GetsediqById { Id = (int)id });
            if (result == null)
                return NotFound();
            
            return PartialView("_DetailsPartial", result);
        }

        public async Task<IActionResult> Edit(long id)
        {
            var result = await _mediator.Send(new GetsediqById { Id = (int)id });
            if (result == null)
                return NotFound();

            var model = new sediqUpdateModel
            {
                RowId = result.RowId,
                sediqCode = result.sediqCode,
                sediqName = result.sediqName,
                StartDate = result.StartDate,
                Description = result.Description
            };
            
            return PartialView("_EditPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(sediqUpdateModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "اطلاعات وارد شده معتبر نیست" });

            var result = await _mediator.Send(new UpdatesediqCommand(model));
            
            if (result.IsSuccess)
                return Json(new { success = true, message = "صدیق با موفقیت به روز شد" });
            
            return Json(new { success = false, message = result.ResponseDesc ?? "خطا در به روز رسانی" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _mediator.Send(new DeletesediqCommand { Id = (int)id });
            
            if (result.IsSuccess)
                return Json(new { success = true, message = "صدیق با موفقیت حذف شد" });
            
            return Json(new { success = false, message = result.ResponseDesc ?? "خطا در حذف" });
        }

    }
}