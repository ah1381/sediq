using Example.Service.Handler.Commands.Student;
using Example.Service.Handler.Queries.Student;
using Example.Service.Models.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sediq.Web.Api.Controllers;

namespace Sediq.Web.Api.Controllers
{
    public class StudentController : BaseController
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetStudentById { Id = id });
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllStudentsQuery());
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StudentUpdateModel dto)
        {
            dto.RowId = id;
            var result = await _mediator.Send(new UpdateStudentCommand(dto));
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteStudentCommand { Id = id });
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        // MVC Actions
        public async Task<IActionResult> List()
        {
            var result = await _mediator.Send(new GetAllStudentsQuery());
            return View(result.IsSuccess ? result.Data : new List<StudentResponseDto>());
        }

        public IActionResult Create()
        {
            return View(new StudentCreateModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentCreateModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var command = new CreateStudentCommand { RequestModel = model };
            var result = await _mediator.Send(command);
            
            if (result.IsSuccess)
            {
                TempData["Success"] = "دانش آموز با موفقیت ثبت شد";
                return RedirectToAction(nameof(List));
            }
            
            ModelState.AddModelError("", result.ResponseDesc);
            return View(model);
        }

        public async Task<IActionResult> Edit(long id)
        {
            var result = await _mediator.Send(new GetStudentById { Id = (int)id });
            if (result == null)
                return NotFound();

            var model = new StudentUpdateModel
            {
                RowId = result.RowId,
                StudentCode = result.StudentCode,
                FirstName = result.FirstName,
                LastName = result.LastName,
                NationalCode = result.NationalCode
            };
            
            return PartialView("_EditPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StudentUpdateModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "اطلاعات وارد شده معتبر نیست" });

            var result = await _mediator.Send(new UpdateStudentCommand(model));
            
            if (result.IsSuccess)
                return Json(new { success = true });
            
            return Json(new { success = false, message = result.ResponseDesc });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _mediator.Send(new DeleteStudentCommand { Id = (int)id });
            
            if (result.IsSuccess)
                return Json(new { success = true });
            
            return Json(new { success = false, message = result.ResponseDesc });
        }
    }
}


