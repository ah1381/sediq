using Raya.Hrm.Shared.Library.ModelS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Domain.Entities
{
    public class ActivityFormEntity : BaseEntity
    {
        [Display(Name = "تاریخ انجام فعالیت")]
        [DataType(DataType.Date)]
        public DateTime ActivityDate { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "برنامه انتخابی")]
        public long SelectedProgramId { get; set; }
        public ProgramEntity SelectedProgram { get; set; }

        [Display(Name = " دانش آموز انتخاب شده")]
        public long SelectedStudentId { get; set; }
        public StudentEntity Student { get; set; }

    }

}
