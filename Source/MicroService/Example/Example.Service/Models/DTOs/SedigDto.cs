using System;
using System.ComponentModel.DataAnnotations;

namespace Example.Service.Models.DTOs
{
    public class sediqBase
    {
        [Display(Name = "کد صدیق")]
        public long sediqCode { get; set; }

        [Display(Name = "نام صدیق")]
        [Required(ErrorMessage = "نام صدیق الزامی است")]
        public string sediqName { get; set; } = string.Empty;

        [Display(Name = "تاریخ شروع")]
        [DataType(DataType.Date)]
        public DateOnly StartDate { get; set; }

        [Display(Name = "توضیحات")]
        public string? Description { get; set; } = string.Empty;
    }

    public class sediqCreateModel : sediqBase
    {
    }

    public class sediqUpdateModel : sediqBase
    {
        public long? RowId { get; set; }
        public string? RandId { get; set; }
    }

    public class sediqDeleteModel
    {
        public long RowId { get; set; }
    }

    public class sediqResponseDto : sediqBase
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