using Raya.Hrm.Shared.Library.Models;
using System.Linq.Expressions;

namespace Raya.Hrm.Shared.Library.Utilities
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyDynamicFilters<T>(this IQueryable<T> query, List<DynamicFilter>? filters)
        {
            if (filters == null || filters.Count == 0)
                return query;

            var parameter = Expression.Parameter(typeof(T), "x");

            Expression? finalExpression = null;

            foreach (var filter in filters)
            {
                Expression? comparison = null;

                // Try list filtering first (for properties like FundBank.BankName)
                if (filter.Field.Contains('.') && filter.Field.Split('.').Length == 2)
                {
                    comparison = CreateListFilterExpression(parameter, filter.Field, filter.Value, filter.Operator);
                }

                // Fallback to regular property filtering
                if (comparison.IsNull())
                {
                    var property = GetNestedProperty(parameter, filter.Field);
                    if (property == null) continue;

                    var constant = Expression.Constant(Convert.ChangeType(filter.Value, property.Type));

                    comparison = filter.Operator.ToLower() switch
                    {
                        "equals" => Expression.Equal(property, constant),
                        "notequals" => Expression.NotEqual(property, constant),
                        "greaterthan" => Expression.GreaterThan(property, constant),
                        "lessthan" => Expression.LessThan(property, constant),
                        "contains" => Expression.Call(property, typeof(string).GetMethod("Contains", new[] { typeof(string) })!, constant),
                        "startswith" => Expression.Call(property, typeof(string).GetMethod("StartsWith", new[] { typeof(string) })!, constant),
                        "endswith" => Expression.Call(property, typeof(string).GetMethod("EndsWith", new[] { typeof(string) })!, constant),
                        _ => null
                    };
                }

                if (comparison.IsNotNull())
                    finalExpression = finalExpression.IsNull()
                        ? comparison
                        : Expression.AndAlso(finalExpression, comparison);
            }

            if (finalExpression.IsNull())
                return query;

            var lambda = Expression.Lambda<Func<T, bool>>(finalExpression, parameter);
            return query.Where(lambda);
        }

        private static Expression? GetNestedProperty(Expression parameter, string propertyPath)
        {
            try
            {
                var properties = propertyPath.Split('.');
                Expression property = parameter;

                foreach (var prop in properties)
                {
                    property = Expression.PropertyOrField(property, prop);
                }

                return property;
            }
            catch
            {
                return null;
            }
        }

        private static Expression? CreateListFilterExpression(Expression parameter, string propertyPath, string value, string @operator)
        {
            try
            {
                var parts = propertyPath.Split('.');
                if (parts.Length != 2) return null;

                var listProperty = Expression.PropertyOrField(parameter, parts[0]);
                var itemProperty = parts[1];

                // Check if it's a list/collection
                var listType = listProperty.Type;
                if (!listType.IsGenericType) return null;

                var itemType = listType.GetGenericArguments()[0];
                var itemParam = Expression.Parameter(itemType, "item");
                var itemProp = Expression.PropertyOrField(itemParam, itemProperty);
                var constant = Expression.Constant(Convert.ChangeType(value, itemProp.Type));

                Expression? itemComparison = @operator.ToLower() switch
                {
                    "equals" => Expression.Equal(itemProp, constant),
                    "contains" => Expression.Call(itemProp, typeof(string).GetMethod("Contains", new[] { typeof(string) })!, constant),
                    _ => null
                };

                if (itemComparison.IsNull()) return null;

                var lambda = Expression.Lambda(itemComparison, itemParam);
                var anyMethod = typeof(Enumerable).GetMethods()
                    .First(m => m.Name == "Any" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(itemType);

                return Expression.Call(anyMethod, listProperty, lambda);
            }
            catch
            {
                return null;
            }
        }
    }
}
