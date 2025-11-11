using Example.Service.Handler.Commands.Program;
using Example.Service.Handler.Queries.Program;
using Example.Service.Models.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Sediq.Web.Api.Controllers
{
    public class ProgramController : BaseController
    {
        private readonly IMediator _mediator;

        public ProgramController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> List()
        {
            var result = await _mediator.Send(new GetAllProgramsQuery());
            return View(result.IsSuccess ? result.Data : new List<ProgramResponseDto>());
        }

        public IActionResult Create()
        {
            return View(new ProgramCreateModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProgramCreateModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var command = new CreateProgramCommand
            {
                Name = model.Name,
                From = model.From,
                To = model.To
            };
            var result = await _mediator.Send(command);
            
            if (result.IsSuccess)
            {
                TempData["Success"] = "برنامه با موفقیت ثبت شد";
                return RedirectToAction(nameof(List));
            }
            
            ModelState.AddModelError("", result.ResponseDesc ?? "خطا در ثبت برنامه");
            return View(model);
        }

        public async Task<IActionResult> Details(long id)
        {
            var result = await _mediator.Send(new GetProgramById { Id = (int)id });
            if (result == null)
                return NotFound();
            
            return PartialView("_DetailsPartial", result);
        }

        public async Task<IActionResult> Edit(long id)
        {
            var result = await _mediator.Send(new GetProgramById { Id = (int)id });
            if (result == null)
                return NotFound();

            var model = new ProgramUpdateModel
            {
                RowId = result.RowId,
                Name = result.Name,
                From = result.From,
                To = result.To
            };
            
            return PartialView("_EditPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProgramUpdateModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "اطلاعات وارد شده معتبر نیست" });

            var result = await _mediator.Send(new UpdateProgramCommand(model));
            
            if (result.IsSuccess)
                return Json(new { success = true, message = "برنامه با موفقیت به روز شد" });
            
            return Json(new { success = false, message = result.ResponseDesc ?? "خطا در به روز رسانی" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _mediator.Send(new DeleteProgramCommand { Id = (int)id });
            
            if (result.IsSuccess)
                return Json(new { success = true, message = "برنامه با موفقیت حذف شد" });
            
            return Json(new { success = false, message = result.ResponseDesc ?? "خطا در حذف" });
        }
    }
}
