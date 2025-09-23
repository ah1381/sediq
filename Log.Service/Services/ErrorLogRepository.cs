using Log.Domain.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Raya.Hrm.Shared.Library.Models.Configs;
using Raya.Hrm.Shared.Library.Models.Raya.Hrm.Shared.Library.Models;

public class ErrorLogRepository : IErrorLogRepository
{
    private readonly IMongoCollection<ErrorModelMongo> _collection;

    public ErrorLogRepository(IOptions<MongoDbConfig> mongoOptions)
    {
        var config = mongoOptions.Value;

        var client = new MongoClient(config.ConnectionString);
        var database = client.GetDatabase(config.DatabaseName);
        _collection = database.GetCollection<ErrorModelMongo>(config.ErrorCollection);
    }

    public async Task<IEnumerable<ErrorModelMongo>> GetLogsAsync(string? serviceName, string? logType)
    {
        var filterBuilder = Builders<ErrorModelMongo>.Filter;
        var filter = FilterDefinition<ErrorModelMongo>.Empty;

        if (!string.IsNullOrEmpty(serviceName))
            filter &= filterBuilder.Eq(x => x.ServiceName, serviceName);

        if (!string.IsNullOrEmpty(logType))
            filter &= filterBuilder.Eq(x => x.LogType, logType);

        return await _collection.Find(filter).ToListAsync();
    }
}
