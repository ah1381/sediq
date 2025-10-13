using Raya.Hrm.Shared.Library.ModelS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Domain.Entities
{
    public class ProgramEntity : BaseEntity
    {
        [Display(Name = "نام برنامه")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "تاریخ شروع")]
        [DataType(DataType.Date)]
        public DateTime? From { get; set; }

        [Display(Name = "تاریخ پایان")]
        [DataType(DataType.Date)]
        public DateTime? To { get; set; }

        public List<ActivityFormEntity> activityForms { get; set; } = new();
        public List<ScoreFormEntity> ScoreForms { get; set; } = new();

    }
}
