using Confluent.Kafka;
using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Raya.Hrm.Shared.Library.Kafka;
using Raya.Hrm.Shared.Library.Models;
using Raya.Hrm.Shared.Library.Models.Base;
using Raya.Hrm.Shared.Library.Models.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Hrm.Shared.Library.GeneralErrorService
{
    public interface IErrorLogInDbService
    {
        Task LogErrorToDbAsync(ErrorModel model);
    }
    public class ErrorLogInDbService : BaseRepository, IErrorLogInDbService
    {

        public ErrorLogInDbService(IOptions<ServicesDbConfig> dbSettings, IOptions<ErrorConfig> errorSettings)
            : base(dbSettings.Value.DefaultConnection)
        {


        }
        public async Task LogErrorToDbAsync(ErrorModel model)
        {


            await SaveErrorToDatabaseAsync(model);
        }

        private async Task SaveErrorToDatabaseAsync(ErrorModel model)
        {
            try
            {
                using var connection = CreateConnection(model.ConnectionType);
                _ = await connection.ExecuteAsync(
                    $"""
                    INSERT INTO error.error_logs
                        (request_id, message, stack_trace, inner_exception, app_name, ip,
                         project_name, service_name, log_type, entity_type, entity_id,
                         entity_rand_id, title, created_by, datetime, device_info, request, response, status)
                    VALUES
                        (@RequestId, @Message, @StackTrace, @InnerException, @AppName, @Ip,
                         @ProjectName, @ServiceName, @LogType, @EntityType, @EntityId,
                         @EntityRandId, @Title, @CreatedBy, @DateTime, @DeviceInfo, @Request, @Response, @Status);
                    """,
                    new
                    {
                        RequestId = model.RequestId,
                        Message = model.Message ?? model.Ex?.Message ?? "No message",
                        StackTrace = model.StackTrace ?? model.Ex?.StackTrace ?? "No stack trace",
                        InnerException = model.InnerException ?? model.Ex?.InnerException?.ToString() ?? "No inner exception",
                        AppName = model.AppName ?? "Unknown",
                        Ip = model.Ip,
                        ProjectName = model.ProjectName,
                        ServiceName = model.ServiceName,
                        LogType = model.LogType ?? "Error",
                        EntityType = model.EntityType,
                        EntityId = model.EntityId,
                        EntityRandId = model.EntityRandId,
                        Title = model.Title ?? "Unhandled Exception",
                        CreatedBy = model.CreatedBy ?? "System",
                        DateTime = model.DateTime ?? DateTime.UtcNow,
                        DeviceInfo = model.DeviceInfo,
                        Request = model.Request,
                        Response = model.Response,
                        Status = model.Status ?? "Failed"
                    });
            }
            catch (Exception e)
            {
                Console.WriteLine($"Cannot insert error to DB: {e.Message}\n{e.StackTrace}");
            }

        }



    }
}