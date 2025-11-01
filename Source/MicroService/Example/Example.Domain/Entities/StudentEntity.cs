using Raya.Hrm.Shared.Library.ModelS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Domain.Entities
{
    public enum Gender
    {
        [Display(Name = "مرد")]
        Male,
        [Display(Name = "زن")]
        Female,
    }
    public class StudentEntity : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }


        [Display(Name = "کد شرکت کننده")]
        public string StudentCode { get; set; }

        [Display(Name = "صدیق")]
        public long SediqCode{ get; set; }

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
        public List<ImagesEntity?> Photos { get; set; } = new();
        [Display(Name = "شماره تلفن‌ها")]
        public virtual List<PhoneNumberEntity?> PhoneNumbers { get; set; } = new();

        [Display(Name = "صدیق")]
        public long? sediqRowId { get; set; }
        public virtual SediqEntity sediq { get; set; }
        public virtual List<ActivityFormEntity> ActivityForms { get; set; } = new();
        public virtual List<ScoreFormEntity> ScoreForms { get; set; } = new();
    }


}
