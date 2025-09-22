using LoanManagement.Domain.Data;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Raya.Hrm.Shared.Library.GeneralErrorService;
using Raya.Hrm.Shared.Library.GeneralRequestService;
using Raya.Hrm.Shared.Library.Kafka;
using Raya.Hrm.Shared.Library.Models;
using Raya.Hrm.Shared.Library.Models.Configs;

namespace LoanManagement.Service.Services.RequestDataService
{
    public interface IRequestDataService
    {
        Task RequestLog(RequestDataModel model);
    }

    public class RequestDataService(
        IKafkaRequestService kafkaService,
        IRequestInDbService InDbService,
        IRequestLogToFIle toFile,
        IRequestMongoService requestMongoService,
        IOptions<RequestConfig> RequestConfig,
        IOptions<MongoDbConfig> mongoConfig,
        LoggingDbContext loggingDbContext // Added DbContext for separate DB
        ) : IRequestDataService
    {
        public async Task RequestLog(RequestDataModel model)
        {
            var config = RequestConfig.Value;

            var requestJson = JsonConvert.SerializeObject(model);

            if (config.LogInKafka)
            {
                var kafkaModel = new KafkaProducerModel()
                {
                    Message = requestJson,
                    Key = "Request"
                };
                await kafkaService.Produce(kafkaModel);
            }

            if (config.LogInDb)
            {
                await InDbService.LogRequestToDbAsync(model);
            }

            if (config.LoginText)
            {
                await toFile.LogRequestToTextAsync(model);
            }

            if (config.LogInMongo)
            {
                var mongoModel = new RequestDataModelMongo
                {
                    request_id = model.request_id,
                    cr = model.cr,
                    requesttype = model.requesttype,
                    Path = model.Path,
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
                    datetime = model.datetime ?? DateTime.UtcNow,
                    ip = model.ip,
                    device_info = model.device_info,
                    request = model.request,
                    response = model.response,
                    status = model.status
                };

                await requestMongoService.LogRequestAsync(mongoModel);
            }

            // 🔹 Insert into separate PostgreSQL DB
            if (loggingDbContext != null)
            {
                try
                {
                    loggingDbContext.RequestLogs.Add(model);
                    await loggingDbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Cannot insert request to Logging DB: {ex.Message}");
                }
            }

            if (!config.LogInKafka && !config.LogInDb && !config.LoginText && !config.LogInMongo)
            {
                Console.WriteLine(requestJson);
            }
        }
    }
}
