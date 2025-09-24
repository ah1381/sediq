using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Service.Models.DTOs
{
    public class ProgramResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }

    public class ProgramCreateDto
    {
        public string Name { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }

    public class ProgramEditDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }

    public class ProgramDeleteDto
    {
        public int Id { get; set; }
    }
}
