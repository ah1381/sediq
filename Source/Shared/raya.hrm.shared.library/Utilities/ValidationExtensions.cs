using Raya.Hrm.Shared.Library.Models.Base;
using Raya.Hrm.Shared.Library.Models.Exception;
using System.Diagnostics;

namespace Raya.Hrm.Shared.Library.Utilities
{
    public static class ValidationExtensions
    {
        public static bool IsNotNull(this object? obj) => obj != null;
        public static bool IsNull(this object? obj) => obj == null;

        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T>? collection) =>
            collection != null && collection.Any();

        public static bool IsNotNullOrEmpty(this string? str) =>
            !string.IsNullOrEmpty(str);

        // New: IsEmpty for IEnumerable<T> (null or no items)
        public static bool IsEmpty<T>(this IEnumerable<T>? collection) =>
            collection == null || !collection.Any();

        // New: IsEmpty for string (null, empty, or whitespace)
        public static bool IsEmpty(this string? str) =>
            string.IsNullOrEmpty(str);

        [DebuggerStepThrough]
        public static void NotNull(object arg, string argName, string message = null)
        {
            if (arg == null)
                throw new BpcValidationException(argName, message ?? $"فیلد {argName} نمی تواند خالی باشد.");
        }

        [DebuggerStepThrough]
        public static void NotNullAndEmpty(string arg, string argName, string message = null)
        {
            if (arg == null || string.IsNullOrEmpty(arg))
                throw new BpcValidationException(argName, message ?? $"فیلد {argName} نمی تواند خالی باشد.");
        }

        [DebuggerStepThrough]
        public static void NotEmpty(string arg, string argName, string message = null)
        {
            if (string.IsNullOrEmpty(arg))
                throw new BpcValidationException(argName, message ?? $"فیلد {argName} نمی تواند خالی باشد.");
        }

        [DebuggerStepThrough]
        public static void NotEmpty<T>(ICollection<T> arg, string argName, string message = null)
        {
            if (arg == null || !arg.Any())
                throw new BpcValidationException(argName, message ?? $"لیست {argName} نمی تواند خالی باشد.");
        }

        [DebuggerStepThrough]
        public static void NotNegative<T>(T arg, string argName, string message = null) where T : struct, IComparable<T>
        {
            if (arg.CompareTo(default(T)) < 0)
                throw new BpcValidationException(argName, message ?? $"فیلد {argName} نمی تواند مقدار منفی داشته باشد.");
        }

        [DebuggerStepThrough]
        public static void NotNegativeAndZero<T>(T arg, string argName, string message = null) where T : struct, IComparable<T>
        {
            if (arg.CompareTo(default(T)) <= 0)
                throw new BpcValidationException(argName, message ?? $"مقدار فیلد {argName} نمی تواند صفر یا منفی باشد.");
        }

        [DebuggerStepThrough]
        public static void IsPositive<T>(T arg, string argName, string message = null) where T : struct, IComparable<T>
        {
            if (arg.CompareTo(default(T)) < 1)
                throw new BpcValidationException(argName, message ?? $"مقدار فیلد {argName} نمی تواند منفی باشد.");
        }

        [DebuggerStepThrough]
        public static void InRange<T>(T arg, T min, T max, string argName, string message = null) where T : struct, IComparable<T>
        {
            if (arg.CompareTo(min) < 0 || arg.CompareTo(max) > 0)
                throw new BpcValidationException(argName, message ?? $"مقدار فیلد {argName} باید بین مقدار {min} و {max} باشد.");
        }

        [DebuggerStepThrough]
        public static void IsTrue(bool arg, string argName, string message = null)
        {
            if (!arg)
                throw new BpcValidationException(argName, message ?? $"مقدار فیلد {argName} نمی تواند رد باشد.");
        }

        [DebuggerStepThrough]
        public static void IfNotNull<T>(this T? obj, Action<T> action) where T : class
        {
            if (obj != null)
                action(obj);
        }

        [DebuggerStepThrough]
        public static TResult IfNotNull<T, TResult>(this T? obj, Func<T, TResult> func, TResult defaultValue = default) where T : class
        {
            return obj != null ? func(obj) : defaultValue;
        }

        [DebuggerStepThrough]
        public static CustomActionResult<TResponse> ToErrorResult<TResponse>(this object obj, string message, Func<bool> condition = null)
        {
            var shouldCreateError = condition?.Invoke() ?? (obj == null);

            return shouldCreateError ? new CustomActionResult<TResponse>
            {
                IsSuccess = false,
                ResponseDesc = message,
                ResponseType = 1
            } : null;
        }
    }
}