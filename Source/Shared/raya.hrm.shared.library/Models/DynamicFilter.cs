using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Hrm.Shared.Library.Models
{
    public class DynamicFilter
    {
        public string Field { get; set; } = string.Empty; // مثل "FundType" یا "CreatedBy"
        public string Operator { get; set; } = "Equals"; //// "equals" "notequals" "greaterthan""lessthan" "contains" "startswith"  "endswith" 
        public string? Value { get; set; } // مقدار فیلتر
    }
}