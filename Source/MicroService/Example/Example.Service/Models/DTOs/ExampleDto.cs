using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Service.Models.DTOs
{
    public class ExampleDto
    {
        public string Name { get; set; } = string.Empty;
        public long? RowId { get; set; }
        public string? RandId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public short? RevSeq { get; set; }
        public short? Status { get; set; }
    }
}
