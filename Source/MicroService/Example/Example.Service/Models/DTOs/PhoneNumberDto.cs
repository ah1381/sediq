using Example.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Example.Service.Models.DTOs
{
    public class PhoneNumberBase
    {
        [Display(Name = "شماره تلفن")]
        public string Number { get; set; } = string.Empty;

        [Display(Name = "مالکیت")]
        public PhoneOwnership Ownership { get; set; }


    }

    public class PhoneNumberCreateModel : PhoneNumberBase
    {
    }

    public class PhoneNumberUpdateModel : PhoneNumberBase
    {
        public long? RowId { get; set; }
        public string? RandId { get; set; }
        [Display(Name = "کد دانش آموزی")]
        public long StudentId { get; set; }
    }

    public class PhoneNumberDeleteModel
    {
        public long RowId { get; set; }
    }

    public class PhoneNumberResponseDto : PhoneNumberBase
    {
        public long RowId { get; set; }
        public string? RandId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public short? RevSeq { get; set; }
        public short? Status { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        [Display(Name = "کد دانش آموزی")]
        public long StudentId { get; set; }
    }
}