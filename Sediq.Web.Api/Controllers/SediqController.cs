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
            return View(result.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(sediqCreateModel model)
        {
            if (ModelState.IsValid)
            {
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
                    TempData["Success"] = "صدیق با موفقیت ایجاد شد";
                    return RedirectToAction("List");
                }
                ModelState.AddModelError("", "خطا در ایجاد صدیق");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetsediqById { Id = id });
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] sediqUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new UpdatesediqCommand(model));
                return Json(new { success = result.IsSuccess, message = result.IsSuccess ? "صدیق با موفقیت ویرایش شد" : "خطا در ویرایش صدیق" });
            }
            return Json(new { success = false, message = "اطلاعات وارد شده معتبر نیست" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeletesediqCommand { Id = id });
            return Json(new { success = result.IsSuccess, message = result.IsSuccess ? "صدیق با موفقیت حذف شد" : "خطا در حذف صدیق" });
        }

        public async Task<IActionResult> Update(long id)
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

    }
}