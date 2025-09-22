using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Raya.Hrm.Shared.Library.Models;
using Raya.Hrm.Shared.Library.Models.Configs;

namespace Raya.Hrm.Shared.Library.GeneralRequestService
{
    public interface IRequestMongoService
    {
        Task LogRequestAsync(RequestDataModelMongo model);
    }

    public class RequestMongoService : IRequestMongoService
    {
        private readonly IMongoCollection<BsonDocument> _collection;

        public RequestMongoService(IOptions<MongoDbConfig> mongoConfig)
        {
            var config = mongoConfig.Value;

            if (string.IsNullOrWhiteSpace(config.ConnectionString))
                throw new ArgumentNullException(nameof(config.ConnectionString), "Mongo connection string is null");

            if (string.IsNullOrWhiteSpace(config.DatabaseName))
                throw new ArgumentNullException(nameof(config.DatabaseName), "Mongo database name is null");

            if (string.IsNullOrWhiteSpace(config.RequestCollection))
                throw new ArgumentNullException(nameof(config.RequestCollection), "Mongo request collection is null");

            var client = new MongoClient(config.ConnectionString);
            var database = client.GetDatabase(config.DatabaseName);
            _collection = database.GetCollection<BsonDocument>(config.RequestCollection);
        }

        public async Task LogRequestAsync(RequestDataModelMongo model)
        {
            try
            {
                var doc = new BsonDocument
                {
                    ["Id"] = model.request_id.ToString(),      // Guid → string
                    ["Cr"] = model.cr.ToString("o"),           // DateTime → ISO 8601 string
                    ["RequestType"] = model.requesttype ?? "Unknown",
                    ["Path"] = model.Path ?? "Unknown",
                    ["QueryString"] = model.queryString ?? "{}",
                    ["Headers"] = model.headers ?? "{}",
                    ["Body"] = model.body ?? "{}",
                    ["ClientIp"] = model.clientIp ?? "Unknown",
                    ["UserAgent"] = model.userAgent ?? "Unknown",
                    ["AppName"] = model.appName ?? "Unknown",
                    ["ProjectName"] = model.project_name ?? "Unknown",
                    ["ServiceName"] = model.service_name ?? "Unknown",
                    ["LogType"] = model.log_type ?? "Request",
                    ["EntityType"] = model.entity_type ?? "Unknown",
                    ["EntityId"] = model.entity_id ?? "Unknown",
                    ["EntityRandId"] = model.entity_rand_id ?? "Unknown",
                    ["Title"] = model.title ?? "Request Log",
                    ["Message"] = model.message ?? "No message",
                    ["CreatedBy"] = model.created_by ?? "System",
                    ["DateTime"] = model.datetime ?? DateTime.UtcNow,
                    ["Ip"] = model.ip ?? "Unknown",
                    ["DeviceInfo"] = model.device_info ?? "Unknown",
                    ["Request"] = model.request ?? "{}",
                    ["Response"] = model.response ?? "{}",
                    ["Status"] = model.status ?? "Completed"
                };

                await _collection.InsertOneAsync(doc);
                Console.WriteLine($"Request logged to MongoDB: {model.request_id}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Cannot insert request to MongoDB: {e.Message}\n{e.StackTrace}");
            }
        }
    }
}
