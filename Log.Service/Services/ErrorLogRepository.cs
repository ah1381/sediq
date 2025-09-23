using Log.Domain.Entities;
using Log.Domain.Interfaces;
using MongoDB.Driver;
using Raya.Hrm.Shared.Library.Models.Raya.Hrm.Shared.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.Service.Services
{
    public class ErrorLogRepository : IErrorLogRepository
    {
        private readonly IMongoCollection<ErrorModelMongo> _collection;

        public ErrorLogRepository(string connectionString, string dbName, string collectionName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(dbName);
            _collection = database.GetCollection<ErrorModelMongo>(collectionName);
        }

        public async Task InsertAsync(ErrorModelMongo log) =>
            await _collection.InsertOneAsync(log);

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
}
