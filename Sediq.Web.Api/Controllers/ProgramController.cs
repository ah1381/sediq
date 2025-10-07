using Example.Service.Handler.Commands.Program;
using Example.Service.Handler.Queries.Program;
using Example.Service.Models.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Sediq.Web.Api.Controllers
{
    public class ProgramController : Controller
    {
        private readonly IMediator _mediator;

        public ProgramController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProgramCreateDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new CreateProgramCommand { RequestModel = model });
                if (result.IsSuccess)
                {
                    TempData["Success"] = "برنامه با موفقیت ایجاد شد";
                    return RedirectToAction("Create");
                }
                ModelState.AddModelError("", "خطا در ایجاد برنامه");
            }
            return View(model);
        }

        public async Task<IActionResult> List()
        {
            var result = await _mediator.Send(new GetAllProgramsQuery());
            return View(result);
        }
    }
}