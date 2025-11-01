using System;
using System.ComponentModel.DataAnnotations;

namespace Example.Service.Models.DTOs
{
    public class ImagesBase
    {
        [Display(Name = "آدرس عکس")]
        [DataType(DataType.ImageUrl)]
        public string PhotoUrl { get; set; } = string.Empty;

        [Display(Name = "توضیحات عکس")]
        public string? Description { get; set; } = string.Empty;


    }

    public class ImagesCreateModel : ImagesBase
    {
    }

    public class ImagesUpdateModel : ImagesBase
    {
        public long? RowId { get; set; }
        public string? RandId { get; set; }
        [Display(Name = "شناسه دانش آموز")]
        public long StudentId { get; set; }
    }

    public class ImagesDeleteModel
    {
        public long RowId { get; set; }
    }

    public class ImagesResponseModel : ImagesBase
    {
        public long RowId { get; set; }
        public string? RandId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public short? RevSeq { get; set; }
        public short? Status { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        [Display(Name = "شناسه دانش آموز")]
        public long StudentId { get; set; }
    }
}