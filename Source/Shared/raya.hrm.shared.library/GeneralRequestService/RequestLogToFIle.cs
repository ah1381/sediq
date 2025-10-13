using Microsoft.Extensions.Options;
using Raya.Hrm.Shared.Library.Models;
using Raya.Hrm.Shared.Library.Models.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Raya.Hrm.Shared.Library.GeneralRequestService
{
    public interface IRequestLogToFIle
    {
        Task LogRequestToTextAsync(RequestDataModel model);

    }
    public class RequestLogToFIle : IRequestLogToFIle
    {
        private readonly RequestConfig _requestConfig;
        private readonly string _logDirectory;
        public RequestLogToFIle(IOptions<DbConfigs> dbSettings, IOptions<RequestConfig> requestSettings)
        {
            _requestConfig = requestSettings.Value;
            _logDirectory = Path.Combine(_requestConfig.LogDirectory, "");
            if (!Directory.Exists(_logDirectory))
                Directory.CreateDirectory(_logDirectory);
        }
        public async Task LogRequestToTextAsync(RequestDataModel model)
        {
            try
            {
                string fileName = $"Log_{DateTime.UtcNow:yyyyMMdd_HHmmss}.txt";
                string filePath = Path.Combine(_logDirectory, fileName);

                var errorDetails = GetRequestDetails(model);

                await File.WriteAllTextAsync(filePath, errorDetails);
            }
            catch (Exception e)
            {
                Console.WriteLine("can not insert Request to file//// ");
            }


        }

        private string GetRequestDetails(RequestDataModel log)
        {
            var sb = new StringBuilder();
            sb.AppendLine("------ Incoming Request ------");
            sb.AppendLine($"Uniq Request ID : {log.request_id}");
            sb.AppendLine($"cr       : {log.cr:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine($"request type     : {log.requesttype}");
            sb.AppendLine($"Path       : {log.Path}");

            if (!string.IsNullOrEmpty(log.queryString))
                sb.AppendLine($"Query      : {log.queryString}");

            if (!string.IsNullOrEmpty(log.clientIp))
                sb.AppendLine($"Client IP  : {log.clientIp}");

            if (!string.IsNullOrEmpty(log.userAgent))
                sb.AppendLine($"UserAgent  : {log.userAgent}");

            sb.AppendLine("Headers    :");
            if (!string.IsNullOrEmpty(log.headers))
            {
                try
                {
                    var headers = JsonSerializer.Deserialize<Dictionary<string, string>>(log.headers);
                    foreach (var h in headers!)
                        sb.AppendLine($"    {h.Key}: {h.Value}");
                }
                catch
                {
                    sb.AppendLine($"    (Error parsing headers JSON)");
                    sb.AppendLine($"    Raw: {log.headers}");
                }
            }

            if (!string.IsNullOrEmpty(log.body))
            {
                sb.AppendLine("Body:");
                sb.AppendLine(log.body);
            }

            sb.AppendLine("------------------------------");
            return sb.ToString();

        }
    }
}
