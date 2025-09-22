using Microsoft.Extensions.Options;
using Raya.Hrm.Shared.Library.Models;
using Raya.Hrm.Shared.Library.Models.Configs;

namespace Raya.Hrm.Shared.Library.GeneralErrorService
{
    public interface IErrorLogToFIle
    {
        Task LogErrorToTextAsync(ErrorModel model);
    }
    public class ErrorLogToFIle : IErrorLogToFIle
    {
        private readonly ErrorConfig _errorConfig;
        private readonly string _logDirectory;
        public ErrorLogToFIle(IOptions<DbConfigs> dbSettings, IOptions<ErrorConfig> errorSettings)
        {
            _errorConfig = errorSettings.Value;
            _logDirectory = Path.Combine(_errorConfig.LogDirectory, "");
            if (!Directory.Exists(_logDirectory))
                Directory.CreateDirectory(_logDirectory);
        }
        public async Task LogErrorToTextAsync(ErrorModel model)
        {
            try
            {
                string fileName = $"Log_{DateTime.UtcNow:yyyyMMdd_HHmmss}.txt";
                string filePath = Path.Combine(_logDirectory, fileName);

                var errorDetails = GetErrorDetails(model);

                await File.WriteAllTextAsync(filePath, errorDetails);
            }
            catch (Exception e)
            {
                Console.WriteLine("can not insert error to file//// "
                                  + model.Ex.Message + model.Ex.StackTrace);

            }


        }
        private string GetErrorDetails(ErrorModel model)
        {
            var exception = model.Ex;
            var message = exception?.Message ?? model.Message ?? "No message";
            var stackTrace = exception?.StackTrace ?? "No stack trace";
            if (stackTrace.Length > 4000)
                stackTrace = stackTrace[..3999];

            var innerException = exception?.InnerException?.ToString() ?? "No inner exception";

            return $"""
            Timestamp: {DateTime.UtcNow}
            Message: {message}
            StackTrace: {stackTrace}
            InnerException: {innerException}
            AppName: {model.AppName}
            Ip: {model.Ip}
            """;
        }
    }
}
