using System;
using System.ComponentModel.DataAnnotations;

namespace Example.Service.Models.DTOs
{
    public class ProgramBase
    {
        [Display(Name = "نام برنامه")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "تاریخ شروع")]
        [DataType(DataType.Date)]
        public DateTime? From { get; set; }

        [Display(Name = "تاریخ پایان")]
        [DataType(DataType.Date)]
        public DateTime? To { get; set; }
    }

    public class ProgramCreateModel : ProgramBase
    {
    }

    public class ProgramUpdateModel : ProgramBase
    {
        public long? RowId { get; set; }
        public string? RandId { get; set; }
    }

    public class ProgramDeleteModel
    {
        public long RowId { get; set; }
    }

    public class ProgramResponseDto : ProgramBase
    {
        public long RowId { get; set; }
        public string? RandId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public short? RevSeq { get; set; }
        public short? Status { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }
}