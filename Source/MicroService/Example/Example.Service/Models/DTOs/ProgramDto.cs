using Example.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Example.Service.Models.DTOs
{
    public class ProgramResponseDto
    {
        [Display(Name = "شناسه")]
        public long Id { get; set; }

        [Display(Name = "نام برنامه")]
        public string Name { get; set; }

        [Display(Name = "تاریخ شروع")]
        [DataType(DataType.Date)]
        public DateTime From { get; set; }

        [Display(Name = "تاریخ پایان")]
        [DataType(DataType.Date)]
        public DateTime To { get; set; }

        public List<ActivityFormEntity> activityForms { get; set; } = new();
    }

    public class ProgramCreateDto
    {
        [Display(Name = "نام برنامه")]
        [Required(ErrorMessage = "وارد کردن نام برنامه الزامی است")]
        public string Name { get; set; }

        [Display(Name = "تاریخ شروع")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "تاریخ شروع الزامی است")]
        public DateTime From { get; set; }

        [Display(Name = "تاریخ پایان")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "تاریخ پایان الزامی است")]
        public DateTime To { get; set; }
    }

    public class ProgramEditDto
    {
        [Display(Name = "شناسه")]
        [Required(ErrorMessage = "شناسه الزامی است")]
        public long Id { get; set; }

        [Display(Name = "نام برنامه")]
        [Required(ErrorMessage = "وارد کردن نام برنامه الزامی است")]
        public string Name { get; set; }

        [Display(Name = "تاریخ شروع")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "تاریخ شروع الزامی است")]
        public DateTime From { get; set; }

        [Display(Name = "تاریخ پایان")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "تاریخ پایان الزامی است")]
        public DateTime To { get; set; }
    }

    public class ProgramDeleteDto
    {
        [Display(Name = "شناسه")]
        [Required(ErrorMessage = "شناسه الزامی است")]
        public long Id { get; set; }
    }
}
