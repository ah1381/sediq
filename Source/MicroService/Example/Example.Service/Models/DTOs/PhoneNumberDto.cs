using Example.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Example.Service.Models.DTOs
{
    public class PhoneNumberResponseDto
    {
        [Display(Name = "شناسه")]
        public long Id { get; set; }

        [Display(Name = "شماره تلفن")]
        public string Number { get; set; }

        [Display(Name = "مالکیت")]
        public PhoneOwnership Ownership { get; set; }

        [Display(Name = "کد دانش آموزی")]
        public int StudentId { get; set; }
    }

    public class PhoneNumberCreateDto
    {
        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "وارد کردن شماره تلفن الزامی است")]
        public string Number { get; set; }

        [Display(Name = "مالکیت")]
        [Required(ErrorMessage = "انتخاب مالکیت الزامی است")]
        public PhoneOwnership Ownership { get; set; }

        [Display(Name = "کد دانش آموزی")]
        public int StudentId { get; set; }
    }

    public class PhoneNumberEditDto
    {
        [Display(Name = "شناسه")]
        [Required(ErrorMessage = "شناسه الزامی است")]
        public long Id { get; set; }

        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "وارد کردن شماره تلفن الزامی است")]
        public string Number { get; set; }

        [Display(Name = "مالکیت")]
        [Required(ErrorMessage = "انتخاب مالکیت الزامی است")]
        public PhoneOwnership Ownership { get; set; }

        [Display(Name = "کد دانش آموزی")]
        public int StudentId { get; set; }
    }

    public class PhoneNumberDeleteDto
    {
        [Display(Name = "شناسه")]
        [Required(ErrorMessage = "شناسه الزامی است")]
        public long Id { get; set; }
    }
}
