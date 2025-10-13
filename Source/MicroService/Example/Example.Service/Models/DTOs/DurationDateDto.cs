using System;
using System.ComponentModel.DataAnnotations;

namespace Example.Service.Models.DTOs
{
    public class DurationDateEntityBase
    {
        [Display(Name = "تاریخ شروع")]
        [DataType(DataType.Date)]
        public DateOnly StartDur { get; set; }

        [Display(Name = "تاریخ پایان")]
        [DataType(DataType.Date)]
        public DateOnly EndDur { get; set; }

        [Display(Name = "نام بازه")]
        public string Name { get; set; } = string.Empty;
    }

    public class DurationDateEntityCreateModel : DurationDateEntityBase
    {
    }

    public class DurationDateEntityUpdateModel : DurationDateEntityBase
    {
        public long? RowId { get; set; }
        public string? RandId { get; set; }
    }

    public class DurationDateEntityDeleteModel
    {
        public long RowId { get; set; }
    }

    public class DurationDateEntityResponseDto: DurationDateEntityBase
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