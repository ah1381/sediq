namespace Raya.Hrm.Shared.Library.Enums
{

    public enum UserType
    {
        Admin = 1,
        Normal = 2
    }

    public enum VariableScalesOfMeasurement
    {
        Nominal = 1,       // Categories without any order (e.g., gender, colors) اسمی
                           // Categories with a meaningful order, but not evenly spaced (e.g., rankings) ترتیبی
        Interval = 2,      // Ordered, evenly spaced, but no true zero (e.g., temperature in Celsius) فاصله ای 
                           // Ordered, evenly spaced, with a meaningful zero (e.g., height, weight, age)
    }
    public enum ScoreSom
    {
        Optional = 0,
        Combined = 1
    }
    public enum PointSom
    {
        Optional = 0,
        Combined = 1
    }

    public enum VariableSom
    {
        WithName = 0,
        WithNumber = 1
    }

    public enum RatioSom
    {
        NotPercent = 0,
        Percent = 1
    }

    public enum FundMemeberType
    {
        UserMember = 0,
        OperationalMember = 1
    }


    #region Filters

    public enum ComparisonType
    {
        Equals,
        NotEqual,
        Contains,
        StartsWith,
        EndsWith,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual,
        In
    }

    public enum StringComparisonType
    {
        Equals,
        Contains,
        StartsWith,
        EndsWith
    }

    public enum NumericComparisonType
    {
        Equals,
        GreaterThan,
        LessThan,
        Between,
        GreaterThanOrEqual
    }

    public enum BooleanComparisonType
    {
        IsTrue,
        IsFalse
    }

    public enum ListComparisonType
    {
        In,
        Contains
    }

    public enum JsonComparisonType
    {
        Equals,
        Contains,
        ContainsKey,
        ContainsValue,
        ContainsJson,
        DeepEqualsText,   // For #>> deep text
        DeepEqualsJson,   // For #> deep JSON
        DeepContainsJson  // For @> JSON contains
    }



    #endregion
}
