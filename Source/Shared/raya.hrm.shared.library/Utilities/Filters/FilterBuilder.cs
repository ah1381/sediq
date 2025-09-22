using Dapper;
using Raya.Hrm.Shared.Library.Enums;
using Raya.Hrm.Shared.Library.Utilities.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public static class FilterBuilder
{
    private static readonly Dictionary<string, ComparisonType> SymbolToComparison = new(StringComparer.OrdinalIgnoreCase)
    {
        ["="] = ComparisonType.Equals,
        ["=="] = ComparisonType.Equals,
        ["!="] = ComparisonType.NotEqual,
        ["<>"] = ComparisonType.NotEqual,
        [">"] = ComparisonType.GreaterThan,
        ["<"] = ComparisonType.LessThan,
        [">="] = ComparisonType.GreaterThanOrEqual,
        ["<="] = ComparisonType.LessThanOrEqual,
        ["contains"] = ComparisonType.Contains,
        ["startswith"] = ComparisonType.StartsWith,
        ["endswith"] = ComparisonType.EndsWith,
        ["in"] = ComparisonType.In
    };

    public static (string WhereClause, DynamicParameters Parameters) BuildWhereClause<T>(
        List<T> groups,
        string[] aliases,
        Dictionary<string, string>? columnMappings = null,
        bool useAndBetweenGroups = false)
    {
        var groupClauses = new List<string>();
        var parameters = new DynamicParameters();
        int groupIndex = 0;
        int globalParamCounter = 0;

        foreach (var group in groups)
        {
            var groupConditions = new List<string>();
            var props = group.GetType().GetProperties();

            foreach (var prop in props)
            {
                var filterObj = prop.GetValue(group);
                if (filterObj == null) continue;

                var valueProp = filterObj.GetType().GetProperty("Value");
                var valuesProp = filterObj.GetType().GetProperty("Values");
                var compProp = filterObj.GetType().GetProperty("Comparison");

                if (compProp == null) continue;

                var comparison = compProp.GetValue(filterObj) as string;
                string dbColumn = prop.Name;
                if (columnMappings != null && columnMappings.TryGetValue(prop.Name, out var mappedColumn))
                {
                    dbColumn = mappedColumn;
                }

                string paramPrefix = $"{dbColumn.Replace(".", "_").Replace("\"", "")}_{groupIndex}_{globalParamCounter++}";

                var (condition, dynParams) = GetConditionWithPrefix(dbColumn, filterObj, paramPrefix);

                parameters.AddDynamicParams(dynParams);

                groupConditions.Add(condition);
            }

            if (groupConditions.Count > 0)
            {
                groupClauses.Add("(" + string.Join(" AND ", groupConditions) + ")");
            }

            groupIndex++;
        }

        if (groupClauses.Count == 0)
            return ("", parameters);

        string combinedGroups = useAndBetweenGroups
            ? string.Join(" AND ", groupClauses)
            : string.Join(" OR ", groupClauses);

        var whereClause = " WHERE " + combinedGroups;
        return (whereClause, parameters);
    }

    private static (string condition, DynamicParameters parameters) GetConditionWithPrefix(string column, object filterObj, string paramPrefix)
    {
        return GetCondition(column, filterObj, paramPrefix);
    }

    public static (string condition, DynamicParameters parameters) GetCondition(string column, object filterObj, string paramPrefix)
    {
        var parameters = new DynamicParameters();

        var type = filterObj.GetType();

        var compProp = type.GetProperty("Comparison");
        var valueProp = type.GetProperty("Value");
        var valuesProp = type.GetProperty("Values");
        var valueToProp = type.GetProperty("ValueTo"); // for between

        if (compProp == null)
            throw new ArgumentException("Filter object must have Comparison property");

        var comparison = compProp.GetValue(filterObj)?.ToString();
        var rawValue = valueProp?.GetValue(filterObj);
        var value = rawValue is JsonElement jsonElement ? jsonElement.ToString() : rawValue;
        var valueTo = valueToProp?.GetValue(filterObj);

        // Special detection: Check if Value is a list (string[])
        if (value is IEnumerable<string> stringList && stringList.Any())
        {
            parameters.Add(paramPrefix, stringList.ToArray());

            return comparison switch
            {
                "In" => ($"{column} = ANY(@{paramPrefix})", parameters),
                "Contains" => ($"{column} @> @{paramPrefix}::text[]", parameters), // <-- Use array containment operator here
                _ => throw new NotImplementedException($"Unsupported ListComparisonType: {comparison}")
            };
        }

        // ListFilter: using 'Values' property
        if (valuesProp != null && valuesProp.GetValue(filterObj) is IEnumerable<object> valuesList && valuesList.Any())
        {
            // Convert all values to string[] (Postgres text[])
            var stringValues = valuesList.Select(v => v?.ToString() ?? "").ToArray();

            parameters.Add(paramPrefix, stringValues);

            return comparison switch
            {
                "In" => ($"{column} = ANY(@{paramPrefix})", parameters),
                "Contains" => ($"{column} @> @{paramPrefix}::text[]", parameters), // <-- Use array containment operator here
                _ => throw new NotImplementedException($"Unsupported ListComparisonType: {comparison}")
            };
        }

        switch (type.Name)
        {
            case "StringFilter":
                parameters.Add(paramPrefix, value);
                return comparison switch
                {
                    "Equals" => ($"{column} = @{paramPrefix}", parameters),
                    "NotEqual" => ($"{column} <> @{paramPrefix}", parameters),
                    "Contains" => ($"{column} ILIKE '%' || @{paramPrefix} || '%'", parameters),
                    "StartsWith" => ($"{column} ILIKE @{paramPrefix} || '%'", parameters),
                    "EndsWith" => ($"{column} ILIKE '%' || @{paramPrefix}", parameters),
                    _ => throw new NotImplementedException($"Unsupported StringComparisonType: {comparison}")
                };

            case "NumberFilter`1":
                if (value == null)
                    throw new ArgumentException("NumberFilter must have a Value");

                parameters.Add(paramPrefix, value);

                return comparison switch
                {
                    "Equals" => ($"{column} = @{paramPrefix}", parameters),
                    "NotEqual" => ($"{column} <> @{paramPrefix}", parameters),
                    "GreaterThan" => ($"{column} > @{paramPrefix}", parameters),
                    "LessThan" => ($"{column} < @{paramPrefix}", parameters),
                    "GreaterThanOrEqual" => ($"{column} >= @{paramPrefix}", parameters),
                    "Between" when valueTo != null => CallBuildBetweenCondition(column, value, valueTo, parameters, paramPrefix),
                    _ => throw new NotImplementedException($"Unsupported NumericComparisonType: {comparison}")
                };

            case "BooleanFilter":
                return comparison switch
                {
                    "IsTrue" => ($"{column} = TRUE", parameters),
                    "IsFalse" => ($"{column} = FALSE", parameters),
                    _ => throw new NotImplementedException($"Unsupported BooleanComparisonType: {comparison}")
                };

            case "JsonFilter":
                var pathProp = type.GetProperty("Path");
                var returnAsTextProp = type.GetProperty("ReturnAsText");
                if (pathProp == null)
                    throw new ArgumentException("JsonFilter must have Path property");

                var path = pathProp.GetValue(filterObj) as string[];
                bool returnAsText = returnAsTextProp != null && (bool)returnAsTextProp.GetValue(filterObj);

                if (path == null || path.Length == 0)
                    throw new ArgumentException("JsonFilter.Path must not be empty");

                string jsonPathSql;
                if (path.Length == 1)
                {
                    jsonPathSql = returnAsText
                        ? $"{column} ->> '{path[0]}'"
                        : $"{column} -> '{path[0]}'";
                }
                else
                {
                    var quotedPath = string.Join(",", path.Select(p => p.Replace("\"", "\"\"")));
                    var pathArray = $"{{{quotedPath}}}";
                    jsonPathSql = returnAsText
                        ? $"{column} #>> '{pathArray}'"
                        : $"{column} #> '{pathArray}'";
                }

                switch (comparison)
                {
                    case "Equals":
                    case "DeepEqualsText":
                        parameters.Add(paramPrefix, value);
                        return ($"{jsonPathSql} = @{paramPrefix}", parameters);

                    case "NotEqual":
                        parameters.Add(paramPrefix, value);
                        return ($"{jsonPathSql} <> @{paramPrefix}", parameters);

                    case "Contains":
                        parameters.Add(paramPrefix, value);
                        return ($"{jsonPathSql} ILIKE '%' || @{paramPrefix} || '%'", parameters);

                    case "ContainsKey":
                        parameters.Add(paramPrefix, path.Last());
                        return ($"{column} ? @{paramPrefix}", parameters);

                    case "ContainsValue":
                        var json = $"{{\"{path.Last()}\":\"{value}\"}}";
                        parameters.Add(paramPrefix, json);
                        return ($"{column}::jsonb @> @{paramPrefix}::jsonb", parameters);

                    case "ContainsJson":
                        parameters.Add(paramPrefix, value);
                        return ($"{column}::jsonb @> @{paramPrefix}::jsonb", parameters);

                    case "DeepEqualsJson":
                        parameters.Add(paramPrefix, value);
                        return ($"{jsonPathSql}::jsonb = @{paramPrefix}::jsonb", parameters);

                    case "DeepContainsJson":
                        parameters.Add(paramPrefix, value);
                        return ($"{jsonPathSql}::jsonb @> @{paramPrefix}::jsonb", parameters);

                    default:
                        throw new NotImplementedException($"Unsupported JsonComparisonType: {comparison}");
                }

            default:
                // Support generic Filter<T> for all types
                if (type.Name.StartsWith("Filter`"))
                {
                    // Fix: If the filter is for a list but reached here, allow it
                    if (value is IEnumerable<string> valList && valList.Any())
                    {
                        parameters.Add(paramPrefix, valList.ToArray());

                        return comparison switch
                        {
                            "In" => ($"{column} = ANY(@{paramPrefix})", parameters),
                            "Contains" => ($"{column} @> @{paramPrefix}::text[]", parameters),
                            _ => throw new NotImplementedException($"Unsupported ListComparisonType: {comparison}")
                        };
                    }

                    if (value == null)
                        throw new ArgumentException("Filter must have a Value");

                    parameters.Add(paramPrefix, value);

                    return comparison switch
                    {
                        "Equals" => ($"{column} = @{paramPrefix}", parameters),
                        "NotEqual" => ($"{column} <> @{paramPrefix}", parameters),
                        "GreaterThan" => ($"{column} > @{paramPrefix}", parameters),
                        "LessThan" => ($"{column} < @{paramPrefix}", parameters),
                        "GreaterThanOrEqual" => ($"{column} >= @{paramPrefix}", parameters),
                        "Between" when valueTo != null => CallBuildBetweenCondition(column, value, valueTo, parameters, paramPrefix),
                        _ => throw new NotImplementedException($"Unsupported NumericComparisonType: {comparison}")
                    };
                }

                throw new NotSupportedException($"Unsupported filter type: {type.Name}");
        }

    }

    private static (string condition, DynamicParameters parameters) BuildBetweenCondition<T>(
        string column, T value1, object value2, DynamicParameters parameters, string paramPrefix) where T : struct
    {
        if (value2 == null)
            throw new ArgumentException("Between comparison requires ValueTo to be set");

        var paramName1 = $"{paramPrefix}_start";
        var paramName2 = $"{paramPrefix}_end";

        parameters.Add(paramName1, value1);
        parameters.Add(paramName2, value2);

        return ($"{column} BETWEEN @{paramName1} AND @{paramName2}", parameters);
    }

    private static (string condition, DynamicParameters parameters) CallBuildBetweenCondition(
        string column, object value1, object value2, DynamicParameters parameters, string paramPrefix)
    {
        if (value1 == null || value2 == null)
            throw new ArgumentException("Between comparison requires both Value and ValueTo to be set");

        Type valueType = value1.GetType();

        if (valueType == typeof(int))
            return BuildBetweenCondition(column, (int)value1, value2, parameters, paramPrefix);

        if (valueType == typeof(long))
            return BuildBetweenCondition(column, (long)value1, value2, parameters, paramPrefix);

        if (valueType == typeof(decimal))
            return BuildBetweenCondition(column, (decimal)value1, value2, parameters, paramPrefix);

        if (valueType == typeof(float))
            return BuildBetweenCondition(column, (float)value1, value2, parameters, paramPrefix);

        if (valueType == typeof(double))
            return BuildBetweenCondition(column, (double)value1, value2, parameters, paramPrefix);

        if (valueType == typeof(DateTime))
            return BuildBetweenCondition(column, (DateTime)value1, value2, parameters, paramPrefix);

        throw new NotSupportedException($"Unsupported Between type: {valueType}");
    }
}
