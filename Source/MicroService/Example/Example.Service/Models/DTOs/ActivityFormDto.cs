using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Example.Service.Models.DTOs
{
    public class ActivityFormBase
    {
        [Display(Name = "تاریخ انجام فعالیت")]
        public DateOnly ActivityDate { get; set; }

        [Display(Name = "شناسه برنامه انتخابی")]
        public long SelectedProgramId { get; set; }

        [Display(Name = "لیست دانش آموزان انتخاب شده")]
        public List<long> SelectedStudentIds { get; set; } = new List<long>();
    }

    public class ActivityFormCreateModel : ActivityFormBase
    {
    }

    public class ActivityFormUpdateModel : ActivityFormBase
    {
        public long? RowId { get; set; }
        public string? RandId { get; set; }
    }

    public class ActivityFormDeleteModel
    {
        public long RowId { get; set; }
    }

    public class ActivityFormResponseDto : ActivityFormBase
    {
        public long RowId { get; set; }
        public string? RandId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public short? RevSeq { get; set; }
        public short? Status { get; set; }
        public string CreatedBy { get; set; } = string.Empty;

        [Display(Name = "نام برنامه انتخابی")]
        public string SelectedProgramName { get; set; } = string.Empty;

        [Display(Name = "نام دانش آموزان")]
        public List<string> StudentNames { get; set; } = new List<string>();
    }
}