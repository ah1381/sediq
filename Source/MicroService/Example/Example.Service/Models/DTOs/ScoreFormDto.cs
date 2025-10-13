using System;
using System.ComponentModel.DataAnnotations;

namespace Example.Service.Models.DTOs
{
    public class ScoreFormBase
    {
        [Display(Name = "شناسه برنامه انتخابی")]
        public long SelectedProgramId { get; set; }

        [Display(Name = "شناسه دانشآموز انتخابی")]
        public long SelectedStudentId { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "نمره")]
        [Range(0, 20, ErrorMessage = "نمره باید بین 0 تا 20 باشد")]
        public decimal Score { get; set; }

        [Display(Name = "شناسه بازه زمانی")]
        public long ActivityDurId { get; set; }
    }

    public class ScoreFormCreateModel : ScoreFormBase
    {
    }

    public class ScoreFormUpdateModel : ScoreFormBase
    {
        public long? RowId { get; set; }
        public string? RandId { get; set; }
    }

    public class ScoreFormDeleteModel
    {
        public long RowId { get; set; }
    }

    public class ScoreFormResponseDto : ScoreFormBase
    {
        public long RowId { get; set; }
        public string? RandId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public short? RevSeq { get; set; }
        public short? Status { get; set; }
        public string CreatedBy { get; set; } = string.Empty;

        [Display(Name = "نام برنامه")]
        public string ProgramName { get; set; } = string.Empty;

        [Display(Name = "نام دانشآموز")]
        public string StudentName { get; set; } = string.Empty;

        [Display(Name = "نام بازه زمانی")]
        public string DurationName { get; set; } = string.Empty;
    }
}