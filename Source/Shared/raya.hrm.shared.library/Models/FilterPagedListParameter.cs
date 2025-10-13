using Raya.Hrm.Shared.Library.Models.Exception;
using Raya.Hrm.Shared.Library.ModelS;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Raya.Hrm.Shared.Library.Models
{
    public static class StringExtensions
    {
        public static IEnumerable<int> AllIndexesOf(this string str, string item)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(item))
                yield break;

            int index = 0;
            while (index < str.Length && (index = str.IndexOf(item, index)) >= 0)
            {
                yield return index;
                index += item.Length;
            }
        }
    }

    public class PagedListParameter
    {
        private const int MaxPageSize = 10000000;
        private int _pageSize = 1; // Default to 1 to avoid zero issues

        public int PageIndex { get; set; } = 1; // Default to 1

        public int PageSize
        {
            get => _pageSize <= 0 ? MaxPageSize : Math.Min(_pageSize, MaxPageSize);
            set => _pageSize = value;
        }

        [JsonIgnore]
        [XmlIgnore]
        public bool IsHasOrderBy => !string.IsNullOrEmpty(OrderBy);

        public string? OrderBy { get; set; }
    }

    public class FilterPagedListParameter<T> : PagedListParameter where T : BaseEntity
    {
        [JsonIgnore]
        [XmlIgnore]
        public bool IsHasFilter => !string.IsNullOrEmpty(Filter);

        public string? Filter { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Filter))
            {
                return;
            }

            var disallowedProps = typeof(T).GetTypeInfo().GetProperties()
                .Where(p => !p.GetCustomAttributes<Attribute>().Any())
                .Select(p => "." + p.Name)
                .ToList();

            if (!disallowedProps.Any())
            {
                return;
            }

            string text;
            try
            {
                text = DynamicExpressionParser.ParseLambda(
                    ParsingConfig.Default,
                    createParameterCtor: false,
                    typeof(T),
                    typeof(bool), // Filters return bool
                    Filter,
                    null).ToString();
            }
            catch (System.Exception ex)
            {
                throw new BpcValidationException(
                    new List<ValidationError> { new ValidationError("Filter", $"Invalid filter syntax: {ex.Message}") });
            }

            var invalidProps = new List<string>();
            foreach (string item in disallowedProps)
            {
                if (!Regex.IsMatch(text, "\\b" + Regex.Escape(item) + "\\b"))
                {
                    continue;
                }

                foreach (int item2 in text.AllIndexesOf(item))
                {
                    int start = Math.Max(0, item2 - 1);
                    int length = Math.Min(item.Length + 2, text.Length - start);
                    string context = text.Substring(start, length);

                    if (!Regex.IsMatch(context, "([\"'])(?:(?=(\\\\?))\\2.)*?\\1"))
                    {
                        invalidProps.Add(item.Substring(1));
                    }
                }
            }

            if (invalidProps.Any())
            {
                var errors = invalidProps.Select(p => new ValidationError("Filter", $"Property [{p}] cannot be used in filter text.")).ToList();
                throw new BpcValidationException(errors);
            }
        }
    }
}