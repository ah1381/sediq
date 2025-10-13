using Raya.Hrm.Shared.Library.ModelS;
using System.ComponentModel.DataAnnotations;

namespace Example.Domain.Entities
{
    public class ScoreFormEntity : BaseEntity
    {
        [Display(Name = "برنامه انتخابی")]
        public long SelectedProgramId { get; set; }
        public ProgramEntity SelectedProgram { get; set; }

        [Display(Name = " شرکت کننده انتخاب شده")]
        public long SelectedStudentId { get; set; }
        public StudentEntity SelectedStudent { get; set; }

        [Display(Name = "توضیح")]
        public string Description { get; set; }

        [Display(Name = "نمره")]
        public Decimal Score { get; set; }

        [Display(Name = "بازه")]
        public long ActivityDurId { get; set; }
        public virtual DurationDateEntity ActivityDur { get; set; }

    }
}
