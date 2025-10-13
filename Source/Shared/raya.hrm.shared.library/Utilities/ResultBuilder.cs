using Raya.Hrm.Shared.Library.Models.Base;

namespace Raya.Hrm.Shared.Library.Utilities
{
    /// <summary>
    /// Factory / fluent builder for <see cref="CustomActionResult{T}"/>.
    /// Usage:
    ///   return Result.Ok(data);
    ///   return Result.Created(data);
    ///   return Result.NotFound("Fund not found");
    ///   return Result.BadRequest("Invalid input");
    /// </summary>
    public static class Result
    {
        // ------------------------------------------------------------------
        // Success factory methods (200-299)
        // ------------------------------------------------------------------
        public static CustomActionResult<T> Ok<T>(T? data = default, string? desc = null)
            => new CustomActionResult<T>
            {
                Data = data,
                IsSuccess = true,
                ResponseType = 200,
                ResponseDesc = desc ?? "Operation succeeded"
            };

        public static CustomActionResult<T> Created<T>(T? data = default, string? desc = null)
            => new CustomActionResult<T>
            {
                Data = data,
                IsSuccess = true,
                ResponseType = 201,
                ResponseDesc = desc ?? "Resource created"
            };

        // ------------------------------------------------------------------
        // Client-error factory methods (400-499)
        // ------------------------------------------------------------------
        public static CustomActionResult<T> BadRequest<T>(string? desc = null, T? data = default)
            => new CustomActionResult<T>
            {
                Data = data,
                IsSuccess = false,
                ResponseType = 400,
                ResponseDesc = desc ?? "Bad request"
            };

        public static CustomActionResult<T> NotFound<T>(string? desc = null, T? data = default)
            => new CustomActionResult<T>
            {
                Data = data,
                IsSuccess = false,
                ResponseType = 404,
                ResponseDesc = desc ?? "Resource not found"
            };

        public static CustomActionResult<T> Conflict<T>(string? desc = null, T? data = default)
            => new CustomActionResult<T>
            {
                Data = data,
                IsSuccess = false,
                ResponseType = 409,
                ResponseDesc = desc ?? "Conflict occurred"
            };

        // ------------------------------------------------------------------
        // Server-error factory methods (500-599)
        // ------------------------------------------------------------------
        public static CustomActionResult<T> InternalError<T>(string? desc = null, T? data = default)
            => new CustomActionResult<T>
            {
                Data = data,
                IsSuccess = false,
                ResponseType = 500,
                ResponseDesc = desc ?? "Internal server error"
            };

        // ------------------------------------------------------------------
        // Generic helper – use when you already know the code
        // ------------------------------------------------------------------
        public static CustomActionResult<T> From<T>(int code, string? desc = null, bool success = true, T? data = default)
            => new CustomActionResult<T>
            {
                Data = data,
                IsSuccess = success,
                ResponseType = code,
                ResponseDesc = desc ?? "Operation completed"
            };

        // In Result.cs (add these methods)
        public static CustomActionResult<bool> Ok(bool data = true, string? desc = null)
            => new CustomActionResult<bool>
            {
                Data = data,
                IsSuccess = true,
                ResponseType = 200,
                ResponseDesc = desc ?? "Operation succeeded"
            };

        public static CustomActionResult<bool> Created(bool data = true, string? desc = null)
            => new CustomActionResult<bool>
            {
                Data = data,
                IsSuccess = true,
                ResponseType = 201,
                ResponseDesc = desc ?? "Resource created"
            };

        public static CustomActionResult<bool> NotFound(string? desc = null, bool data = false)
            => new CustomActionResult<bool>
            {
                Data = data,
                IsSuccess = false,
                ResponseType = 404,
                ResponseDesc = desc ?? "Resource not found"
            };

        public static CustomActionResult<T> WithTotalCount<T>(this CustomActionResult<T> result, int total)
        {
            result.TotalCount = total;
            return result;
        }
    }
}
