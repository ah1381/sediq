using Raya.Hrm.Shared.Library.ModelS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Domain.Entities
{
    public enum PhoneOwnership
    {
        [Display(Name = "همراه")]
        Mobile,
        [Display(Name = "خانه")]
        Home,
        [Display(Name = "پدر")]
        Father,
        [Display(Name = "مادر")]
        Mother
    }

    public class PhoneNumberEntity : BaseEntity
    {
        [Display(Name = "شماره تلفن")]
        public string Number { get; set; } = string.Empty;

        [Display(Name = "مالکیت")]
        public PhoneOwnership Ownership { get; set; }

        [Display(Name = "کد شرکت کنندهی")]
        public long StudentId { get; set; }
        public StudentEntity Student { get; set; }
    }
}
