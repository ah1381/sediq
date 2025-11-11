using Example.Service.Handler.Commands.Student;
using Example.Service.Handler.Queries.Student;
using Example.Service.Models.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Sediq.Web.Api.Controllers
{
    public class StudentController : BaseController
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

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

            var command = new CreateStudentCommand { RequestModel = model };
            var result = await _mediator.Send(command);
            
            if (result.IsSuccess)
            {
                TempData["Success"] = "شرکت کننده با موفقیت ثبت شد";
                return RedirectToAction(nameof(List));
            }
            
            ModelState.AddModelError("", result.ResponseDesc ?? "خطا در ثبت شرکت کننده");
            return View(model);
        }

        public async Task<IActionResult> Details(long id)
        {
            var result = await _mediator.Send(new GetStudentById { Id = (int)id });
            if (result == null)
                return NotFound();
            
            return PartialView("_DetailsPartial", result);
        }

        public async Task<IActionResult> Edit(long id)
        {
            var result = await _mediator.Send(new GetStudentById { Id = (int)id });
            if (result == null)
                return NotFound();

            var model = new StudentUpdateModel
            {
                RowId = result.RowId,
                FirstName = result.FirstName,
                LastName = result.LastName,
                NationalCode = result.NationalCode,
                MembershipDate = result.MembershipDate,
                FatherName = result.FatherName,
                FatherJob = result.FatherJob,
                BirthDate = result.BirthDate,
                FieldOfStudy = result.FieldOfStudy,
                YearStudy = result.YearStudy,
                Gender = result.Gender,
                Address = result.Address,
                EducationStatus = result.EducationStatus,
                Notes = result.Notes
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
                return Json(new { success = true, message = "شرکت کننده با موفقیت به روز شد" });
            
            return Json(new { success = false, message = result.ResponseDesc ?? "خطا در به روز رسانی" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _mediator.Send(new DeleteStudentCommand { Id = (int)id });
            
            if (result.IsSuccess)
                return Json(new { success = true, message = "شرکت کننده با موفقیت حذف شد" });
            
            return Json(new { success = false, message = result.ResponseDesc ?? "خطا در حذف" });
        }
    }
}
