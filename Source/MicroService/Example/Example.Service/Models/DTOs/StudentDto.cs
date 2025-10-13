using Example.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Example.Service.Models.DTOs
{
    public class StudentBase
    {

        public string Username { get; set; }
        public string Password { get; set; }


        [Display(Name = "صدیق")]
        public long SediqCode { get; set; }

        [Display(Name = "نام")]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; } = string.Empty;

        [Display(Name = "تاریخ عضویت")]
        [DataType(DataType.Date)]
        public DateTime? MembershipDate { get; set; }

        [Display(Name = "نام پدر")]
        public string FatherName { get; set; } = string.Empty;

        [Display(Name = "شغل پدر")]
        public string? FatherJob { get; set; } = string.Empty;

        [Display(Name = "تاریخ تولد")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "رشته تحصیلی")]
        public string? FieldOfStudy { get; set; } = string.Empty;

        [Display(Name = "پایه تحصیلی")]
        public string? YearStudy { get; set; } = string.Empty;

        [Display(Name = "جنسیت")]
        public Gender Gender { get; set; }

        [Display(Name = "آدرس")]
        public string? Address { get; set; } = string.Empty;

        [Display(Name = "وضعیت تحصیلی")]
        public string? EducationStatus { get; set; } = string.Empty;

        [Display(Name = "توضیحات اضافی")]
        public string? Notes { get; set; } = string.Empty;

        [Display(Name = "عکس ها")]
        public List<ImagesCreateModel?> Photos { get; set; } = new();

        [Display(Name = "شماره تلفن‌ها")]
        public virtual List<PhoneNumberCreateModel?> PhoneNumbers { get; set; } = new();

    }

    public class StudentCreateModel : StudentBase
    {
    }

    public class StudentUpdateModel : StudentBase
    {
        public long? RowId { get; set; }
        public string? RandId { get; set; }
        public string StudentCode { get; set; }
    }

    public class StudentDeleteModel
    {
        public long RowId { get; set; }
    }

    public class StudentResponseDto : StudentBase
    {
        public long RowId { get; set; }
        public string? RandId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public short? RevSeq { get; set; }
        public short? Status { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string StudentCode { get; set; }

        [Display(Name = "نام کامل")]
        public string FullName => $"{FirstName} {LastName}";
    }
}