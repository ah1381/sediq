using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Raya.Hrm.Shared.Library.Consts;
using Raya.Hrm.Shared.Library.GeneralErrorService;
using Raya.Hrm.Shared.Library.Kafka;
using Raya.Hrm.Shared.Library.Models;
using Raya.Hrm.Shared.Library.Models.Configs;
using Raya.Hrm.Shared.Library.Models.Raya.Hrm.Shared.Library.Models;

namespace Raya.Hrm.Shared.Library.GeneralErrorService

{
    public interface IErrorService
    {
        Task ErrorLog(ErrorModel model);
    }

    public class ErrorService(
        IKafkaErrorService kafkaErrorService,
        IErrorLogInDbService errorInDbService,
        IErrorLogToFIle errorToFile,
        //IErrorMongoService errorMongoService,
        IOptions<ErrorConfig> errorConfig
        //IOptions<MongoDbConfig> mongoConfig,
        //LoggingDbContext loggingDbContext // Added DbContext for separate DB
        ) : IErrorService
    {
        public async Task ErrorLog(ErrorModel model)
        {
            var config = errorConfig.Value;

            model.AppName = Projects.RayaFlowProject;
            var error = JsonConvert.SerializeObject(model);

            if (config.LogInKafka)
            {
                var kafkaModel = new KafkaProducerModel()
                {
                    Message = error,
                    Key = "Error"
                };
                _ = await kafkaErrorService.Produce(kafkaModel);
            }

            if (config.LogInDb)
            {
                await errorInDbService.LogErrorToDbAsync(model);
            }

            if (config.LoginText)
            {
                await errorToFile.LogErrorToTextAsync(model);
            }

            if (config.LogInMongo)
            {
                var mongoModel = new ErrorModelMongo
                {
                    RequestId = model.RequestId ?? Guid.NewGuid(),
                    AppName = model.AppName,
                    Message = model.Message,
                    StackTrace = model.StackTrace,
                    InnerException = model.InnerException,
                    Ip = model.Ip,
                    ProjectName = model.ProjectName,
                    ServiceName = model.ServiceName,
                    LogType = model.LogType,
                    EntityType = model.EntityType,
                    EntityId = model.EntityId,
                    EntityRandId = model.EntityRandId,
                    Title = model.Title,
                    CreatedBy = model.CreatedBy,
                    DateTime = model.DateTime,
                    DeviceInfo = model.DeviceInfo,
                    Request = model.Request,
                    Response = model.Response,
                    Status = model.Status,
                    Ex = model.Ex
                };

                //await errorMongoService.LogErrorAsync(mongoModel);
            }

            //// 🔹 Insert into separate PostgreSQL DB
            //if (loggingDbContext != null)
            //{
            //    try
            //    {
            //        loggingDbContext.ErrorLogs.Add(model);
            //        await loggingDbContext.SaveChangesAsync();
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine($"Cannot insert error to Logging DB: {ex.Message}");
            //    }
            //}

            if (config is { LoginText: false, LogInDb: false, LogInKafka: false, LogInMongo: false })
            {
                Console.WriteLine(error);
            }
        }
    }
}
