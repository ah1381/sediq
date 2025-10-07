using Example.Service.Handler.Commands.Student;
using Example.Service.Handler.Queries.Student;
using Example.Service.Models.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Sediq.Web.Api.Controllers
{
    public class StudentController : Controller
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentCreateDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new CreateStudentCommand { RequestModel = model });
                if (result.IsSuccess)
                {
                    TempData["Success"] = "دانشآموز با موفقیت ایجاد شد";
                    return RedirectToAction("Create");
                }
                ModelState.AddModelError("", "خطا در ایجاد دانشآموز");
            }
            return View(model);
        }

        public async Task<IActionResult> List()
        {
            var result = await _mediator.Send(new GetAllStudentsQuery());
            return View(result);
        }
    }
}