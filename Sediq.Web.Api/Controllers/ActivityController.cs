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
    public class ActivityController : Controller
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
        public async Task<IActionResult> Create(ActivityFormCreateDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new CreateActivityFormCommand { RequestModel = model });
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

            ViewBag.Programs = new SelectList(programs, "Id", "Name");
            ViewBag.Students = new SelectList(students, "Id", "FullName");
        }
    }
}