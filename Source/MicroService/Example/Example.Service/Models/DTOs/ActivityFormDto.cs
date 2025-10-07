using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Example.Service.Models.DTOs
{
    public class ActivityFormResponseDto
    {
        [Display(Name = "شناسه")]
        public long Id { get; set; }

        [Display(Name = "تاریخ انجام فعالیت")]
        public DateTime ActivityDate { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "شناسه برنامه انتخابی")]
        public long SelectedProgramId { get; set; }

        [Display(Name = "نام برنامه انتخابی")]
        public string SelectedProgramName { get; set; }

        [Display(Name = "لیست دانش‌آموزان انتخاب شده")]
        public long SelectedStudentIds { get; set; }

        [Display(Name = "نام دانشآموزان")]
        public string StudentNames { get; set; } 
    }

    public class ActivityFormCreateDto
    {
        [Display(Name = "تاریخ انجام فعالیت")]
        public DateTime ActivityDate { get; set; }

        [Display(Name = "شناسه برنامه انتخابی")]
        public long SelectedProgramId { get; set; }

        [Display(Name = "لیست دانش‌آموزان انتخاب شده")]
        public List<long> SelectedStudentIds { get; set; } = new List<long>();
    }

    public class ActivityFormEditDto
    {
        [Display(Name = "شناسه")]
        public long Id { get; set; }

        [Display(Name = "تاریخ انجام فعالیت")]
        public DateTime ActivityDate { get; set; }

        [Display(Name = "شناسه برنامه انتخابی")]
        public long SelectedProgramId { get; set; }

        [Display(Name = "لیست دانش‌آموزان انتخاب شده")]
        public List<long> SelectedStudentIds { get; set; } = new List<long>();
    }

    public class ActivityFormDeleteDto
    {
        [Display(Name = "شناسه")]
        public long Id { get; set; }
    }
}
