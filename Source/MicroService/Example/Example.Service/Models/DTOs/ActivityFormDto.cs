using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Service.Models.DTOs
{
    public class ActivityFormResponseDto
    {
        public int Id { get; set; }
        public DateTime ActivityDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public int SelectedProgramId { get; set; }
        public string SelectedProgramName { get; set; }
        public List<int> SelectedStudentIds { get; set; } = new List<int>();
    }

    public class ActivityFormCreateDto
    {
        public DateTime ActivityDate { get; set; }
        public int SelectedProgramId { get; set; }
        public List<int> SelectedStudentIds { get; set; } = new List<int>();
    }

    public class ActivityFormEditDto
    {
        public int Id { get; set; }
        public DateTime ActivityDate { get; set; }
        public int SelectedProgramId { get; set; }
        public List<int> SelectedStudentIds { get; set; } = new List<int>();
    }

    public class ActivityFormDeleteDto
    {
        public int Id { get; set; }
    }
}
