using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Raya.Hrm.Shared.Library.Models.Configs;
using Raya.Hrm.Shared.Library.Models.Raya.Hrm.Shared.Library.Models;

namespace Raya.Hrm.Shared.Library.GeneralErrorService
{
    public interface IErrorMongoService
    {
        Task LogErrorAsync(ErrorModelMongo model);
    }

    public class ErrorMongoService : IErrorMongoService
    {
        private readonly IMongoCollection<BsonDocument> _collection;

        public ErrorMongoService(IOptions<MongoDbConfig> mongoConfig)
        {
            var config = mongoConfig.Value;

            if (string.IsNullOrWhiteSpace(config.ConnectionString))
                throw new ArgumentNullException(nameof(config.ConnectionString), "Mongo connection string is null");

            if (string.IsNullOrWhiteSpace(config.DatabaseName))
                throw new ArgumentNullException(nameof(config.DatabaseName), "Mongo database name is null");

            if (string.IsNullOrWhiteSpace(config.ErrorCollection))
                throw new ArgumentNullException(nameof(config.ErrorCollection), "Mongo error collection is null");

            // Ensure proper auth source
            var clientSettings = MongoClientSettings.FromConnectionString(config.ConnectionString);
            clientSettings.ServerSelectionTimeout = TimeSpan.FromSeconds(5); // fail fast if DB unreachable
            var client = new MongoClient(clientSettings);

            var database = client.GetDatabase(config.DatabaseName);
            _collection = database.GetCollection<BsonDocument>(config.ErrorCollection);
        }

        public async Task LogErrorAsync(ErrorModelMongo model)
        {
            try
            {
                var doc = new BsonDocument
                {
                    ["RequestId"] = model.RequestId?.ToString() ?? ObjectId.GenerateNewId().ToString(),
                    ["Message"] = model.Message ?? model.Ex?.Message ?? "No message",
                    ["StackTrace"] = model.StackTrace ?? model.Ex?.StackTrace ?? "No stack trace",
                    ["InnerException"] = model.InnerException ?? model.Ex?.InnerException?.ToString() ?? "No inner exception",
                    ["AppName"] = model.AppName ?? "Unknown",
                    ["Ip"] = model.Ip ?? "Unknown",
                    ["ProjectName"] = model.ProjectName ?? "Unknown",
                    ["ServiceName"] = model.ServiceName ?? "Unknown",
                    ["LogType"] = model.LogType ?? "Error",
                    ["EntityType"] = model.EntityType ?? "Unknown",
                    ["EntityId"] = model.EntityId ?? "Unknown",
                    ["EntityRandId"] = model.EntityRandId ?? "Unknown",
                    ["Title"] = model.Title ?? "Unhandled Exception",
                    ["CreatedBy"] = model.CreatedBy ?? "System",
                    ["DateTime"] = model.DateTime.HasValue
                        ? new BsonDateTime(model.DateTime.Value)
                        : new BsonDateTime(DateTime.UtcNow),
                    ["DeviceInfo"] = model.DeviceInfo ?? "Unknown",
                    ["Request"] = model.Request ?? "{}",
                    ["Response"] = model.Response ?? "{}",
                    ["Status"] = model.Status ?? "Failed"
                };

                await _collection.InsertOneAsync(doc);
                Console.WriteLine("Error logged to MongoDB successfully.");
            }
            catch (MongoException mongoEx)
            {
                Console.WriteLine($"MongoDB error: {mongoEx.Message}\n{mongoEx.StackTrace}");
                throw; // optionally rethrow to catch in higher layer
            }
            catch (Exception e)
            {
                Console.WriteLine($"Cannot insert error to MongoDB: {e.Message}\n{e.StackTrace}");
                throw;
            }
        }
    }
}