using Raya.Hrm.Shared.Library.ModelS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Domain.Entities
{
    public class ImagesEntity : BaseEntity
    {
        [Display(Name = "آدرس عکس")]
        [DataType(DataType.ImageUrl)]
        public string PhotoUrl { get; set; } = string.Empty;

        [Display(Name = "توضیحات عکس")]
        public string Description { get; set; } = string.Empty;

        public long StudentId { get; set; }
        public StudentEntity Student { get; set; }
    }
}
