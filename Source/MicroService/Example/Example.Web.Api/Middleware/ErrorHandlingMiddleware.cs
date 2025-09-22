using Example.Service.Services.ErrorService;
using Example.Service.Services.RequestDataService;
using Microsoft.Extensions.Options;
using Raya.Hrm.Shared.Library.Consts;
using Raya.Hrm.Shared.Library.Models;
using Raya.Hrm.Shared.Library.Models.Configs;
using System.Text.Json;

namespace Example.Web.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;
        private readonly RequestConfig _requestConfig;

        public ErrorHandlingMiddleware(
            RequestDelegate next,
            IServiceProvider serviceProvider,
            IOptions<RequestConfig> requestConfig)
        {
            _next = next;
            _serviceProvider = serviceProvider;
            _requestConfig = requestConfig.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            string requestBody = string.Empty;
            string responseBody = string.Empty;

            try
            {
                if (_requestConfig.Enable)
                    requestBody = await LogRequestAsync(context); // capture requestBody

                await _next(context);
            }
            catch (Exception ex)
            {
                using var scope = _serviceProvider.CreateScope(); // 🔹 create scope
                var errorLogger = scope.ServiceProvider.GetRequiredService<IErrorService>();

                string? clientIp = $"{context.Connection.RemoteIpAddress}:{context.Connection.RemotePort}";
                string? serverIp = $"{context.Connection.LocalIpAddress}:{context.Connection.LocalPort}";

                var requestId = context.Items["RequestId"] as Guid? ?? Guid.NewGuid();

                var error = new ErrorModel
                {
                    RequestId = requestId,
                    AppName = "Example",
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    InnerException = ex.InnerException?.Message,
                    Ip = clientIp,

                    ProjectName = "Example",
                    ServiceName = "WebApi",
                    LogType = "Error",
                    EntityType = "LoanRequest",
                    Title = "Unhandled Exception",
                    CreatedBy = "System",
                    DateTime = DateTime.UtcNow,
                    DeviceInfo = context.Request.Headers["User-Agent"],
                    Request = requestBody,
                    Response = responseBody,
                    Status = "Failed"
                };

                await errorLogger.ErrorLog(error);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Internal server error");
            }
        }

        private async Task<string> LogRequestAsync(HttpContext context)
        {
            context.Request.EnableBuffering();
            var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
            context.Request.Body.Position = 0;

            using var scope = _serviceProvider.CreateScope(); // 🔹 create scope
            var requestDataLogger = scope.ServiceProvider.GetRequiredService<IRequestDataService>();

            var log = new RequestDataModel
            {
                request_id = Guid.NewGuid(),
                cr = DateTime.UtcNow,
                requesttype = context.Request.Method,
                Path = context.Request.Path,
                queryString = context.Request.QueryString.ToString(),
                headers = JsonSerializer.Serialize(
                                context.Request.Headers.ToDictionary(k => k.Key,
                                                                     v => v.Value.ToString())),
                body = body,
                clientIp = context.Connection.RemoteIpAddress?.ToString(),
                userAgent = context.Request.Headers["User-Agent"],
                appName = Projects.RayaFlowProject,
                ConnectionType = "Npgsql",

                project_name = Projects.RayaFlowProject,
                service_name = "Example.WebApi",
                log_type = "Request",
                entity_type = context.Request.Path.ToString().Split('/').Skip(2).FirstOrDefault(),
                entity_id = context.Request.Method == "GET" ? context.Request.Path.ToString().Split('/').Last() : "",
                entity_rand_id = null,
                title = "Incoming Request",
                message = "Captured request log",
                created_by = "system",
                datetime = DateTime.UtcNow,
                ip = context.Connection.RemoteIpAddress?.ToString(),
                device_info = context.Request.Headers["User-Agent"],
                request = body,
                response = null,
                status = "Pending"
            };

            await requestDataLogger.RequestLog(log);
            context.Items["RequestId"] = log.request_id;

            return body; // return requestBody for error logging
        }
    }
}
