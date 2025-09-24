using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Service.Models.DTOs
{
    public class ActivityFormResponseDto
    {
        public long Id { get; set; }
        public DateTime ActivityDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public long SelectedProgramId { get; set; }
        public string SelectedProgramName { get; set; }
        public List<long> SelectedStudentIds { get; set; } = new List<long>();
    }

    public class ActivityFormCreateDto
    {
        public DateTime ActivityDate { get; set; }
        public long SelectedProgramId { get; set; }
        public List<long> SelectedStudentIds { get; set; } = new List<long>();
    }

    public class ActivityFormEditDto
    {
        public long Id { get; set; }
        public DateTime ActivityDate { get; set; }
        public long SelectedProgramId { get; set; }
        public List<long> SelectedStudentIds { get; set; } = new List<long>();
    }

    public class ActivityFormDeleteDto
    {
        public long Id { get; set; }
    }
}
