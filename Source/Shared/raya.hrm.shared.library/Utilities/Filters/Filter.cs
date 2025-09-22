using Raya.Hrm.Shared.Library.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Hrm.Shared.Library.Utilities.Filters
{
    public interface IFilter { }
    public class Filter<T>
    {
        public T Value { get; set; }
        public string Comparison { get; set; } = "=";
    }

    public class StringFilter : IFilter
    {
        public string? Value { get; set; }
        public StringComparisonType Comparison { get; set; }
    }

    public class NumberFilter<T> : IFilter where T : struct
    {
        public T? Value { get; set; }
        public T? ValueTo { get; set; } // for BETWEEN
        public NumericComparisonType Comparison { get; set; }
    }

    public class BooleanFilter : IFilter
    {
        public BooleanComparisonType Comparison { get; set; }
    }

    public class ListFilter : IFilter
    {
        public IEnumerable<string>? Values { get; set; }
        public ListComparisonType Comparison { get; set; }
    }

    public class JsonFilter : IFilter
    {
        public string? Key { get; set; }
        public string[]? Path { get; set; } // Supports nested JSON keys
        public object? Value { get; set; }
        public JsonComparisonType Comparison { get; set; }
        public bool ReturnAsText { get; set; } = true; // true: ->> or #>>, false: -> or #>


    }

}
