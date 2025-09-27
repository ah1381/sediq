using Example.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Example.Service.Models.DTOs
{
    public class StudentResponseDto
    {
        public long Id { get; set; }

        [Display(Name = "کد دانش آموز")]
        public int StudentCode { get; set; }

        [Display(Name = "نام")]
        public string FullName { get; set; } = string.Empty;

        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; } = string.Empty;

        [Display(Name = "تاریخ عضویت")]
        [DataType(DataType.Date)]
        public DateTime MembershipDate { get; set; }

        [Display(Name = "نام پدر")]
        public string FatherName { get; set; } = string.Empty;

        [Display(Name = "شغل پدر")]
        public string FatherJob { get; set; } = string.Empty;

        [Display(Name = "تاریخ تولد")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }


        [Display(Name = "رشته تحصیلی")]
        public string FieldOfStudy { get; set; } = string.Empty;


        [Display(Name = "جنسیت")]
        public Gender Gender { get; set; }

        [Display(Name = "آدرس")]
        public string Address { get; set; } = string.Empty;

        [Display(Name = "وضعیت تحصیلی")]
        public string EducationStatus { get; set; } = string.Empty;

        [Display(Name = "توضیحات اضافی")]
        public string Notes { get; set; } = string.Empty;

        [Display(Name = "عکس ها")]
        public List<string> Photos { get; set; } = new();

        [Display(Name = "شماره تلفن‌ها")]
        public List<PhoneNumberEntity> PhoneNumbers { get; set; } = new();
        public List<ActivityFormEntity> ActivityForms { get; set; } = new();
    }

    public class StudentCreateDto
    {
        [Display(Name = "کد دانش آموز")]
        public int StudentCode { get; set; }

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
        public List<string?> Photos { get; set; } = new();


        [Display(Name = "شماره تلفن‌ها")]
        public List<PhoneNumberCreateDto?> PhoneNumbers { get; set; } = new();
    }

    public class StudentEditDto
    {
        [Required]
        public long Id { get; set; }

        [Display(Name = "کد دانش آموز")]
        public int StudentCode { get; set; }

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
        public List<string?> Photos { get; set; } = new();


        [Display(Name = "شماره تلفن‌ها")]
        public List<PhoneNumberEditDto?> PhoneNumbers { get; set; } = new();
    }

    public class StudentDeleteDto
    {
        [Required]
        public long Id { get; set; }
    }
}
