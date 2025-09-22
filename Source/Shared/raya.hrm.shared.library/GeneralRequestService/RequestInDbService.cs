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
    public interface IRequestInDbService
    {
        Task LogRequestToDbAsync(RequestDataModel model);
    }
    public class RequestInDbService : BaseRepository, IRequestInDbService
    {

        public RequestInDbService(IOptions<ServicesDbConfig> dbSettings, IOptions<RequestConfig> requestSettings)
            : base(dbSettings.Value.DefaultConnection)
        {


        }
        public async Task LogRequestToDbAsync(RequestDataModel model)
        {


            await SaverequestToDatabaseAsync(model);
        }

        private async Task SaverequestToDatabaseAsync(RequestDataModel model)
        {
            try
            {
                using var connection = CreateConnection(model.ConnectionType);
                //_ = await connection.ExecuteAsync(
                //                   $"""
                //    INSERT INTO "error".request_logs 
                //        (id, cr, request_type, path, query_string, headers, body, client_ip, user_agent, app_name)
                //    VALUES 
                //        (@id, @cr, @requesttype, @path, @queryString, @headers, @body, @clientIp, @userAgent, @appName);
                //    """,
                //    new
                //    {
                //        id = model.request_id,
                //        cr = model.cr,
                //        requesttype = model.requesttype,
                //        path = model.Path,
                //        queryString = model.queryString,
                //        headers = model.headers,
                //        body = model.body,
                //        clientIp = model.clientIp,
                //        userAgent = model.userAgent,
                //        appName = model.appName
                //    });
                _ = await connection.ExecuteAsync(
                $"""
                INSERT INTO "error".request_logs 
                    (id, cr, request_type, path, query_string, headers, body, client_ip, user_agent, app_name,
                     project_name, service_name, log_type, entity_type, entity_id, entity_rand_id,
                     title, message, created_by, datetime, ip, device_info, request, response, status)
                VALUES 
                    (@id, @cr, @requesttype, @path, @queryString, @headers, @body, @clientIp, @userAgent, @appName,
                     @project_name, @service_name, @log_type, @entity_type, @entity_id, @entity_rand_id,
                     @title, @message, @created_by, @datetime, @ip, @device_info, @request, @response, @status);
                """,
                new
                {
                    id = model.request_id,
                    cr = model.cr,
                    requesttype = model.requesttype,
                    path = model.Path,
                    queryString = model.queryString,
                    headers = model.headers,
                    body = model.body,
                    clientIp = model.clientIp,
                    userAgent = model.userAgent,
                    appName = model.appName,
                    project_name = model.project_name,
                    service_name = model.service_name,
                    log_type = model.log_type,
                    entity_type = model.entity_type,
                    entity_id = model.entity_id,
                    entity_rand_id = model.entity_rand_id,
                    title = model.title,
                    message = model.message,
                    created_by = model.created_by,
                    datetime = model.datetime,
                    ip = model.ip,
                    device_info = model.device_info,
                    request = model.request,
                    response = model.response,
                    status = model.status
                });




            }
            catch (Exception e)
            {
                Console.WriteLine("can not insert error to db//// ");
            }

        }

    }
}