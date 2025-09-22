using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Raya.Hrm.Shared.Library.Models
{
    public class RequestDataModelMongo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("request_id")]
        public Guid request_id { get; set; } = Guid.NewGuid();

        [BsonElement("cr")]
        public DateTime cr { get; set; } = DateTime.UtcNow;

        [BsonElement("request_type")]
        public string requesttype { get; set; } = null!;

        [BsonElement("path")]
        public string Path { get; set; } = null!;

        [BsonElement("query_string")]
        public string? queryString { get; set; }

        [BsonElement("headers")]
        public string headers { get; set; } = null!;

        [BsonElement("body")]
        public string? body { get; set; }

        [BsonElement("client_ip")]
        public string? clientIp { get; set; }

        [BsonElement("user_agent")]
        public string? userAgent { get; set; }

        [BsonElement("app_name")]
        public string appName { get; set; } = null!;

        [BsonIgnore]
        public string? ConnectionType { get; set; }

        // 🔹 Additional fields
        [BsonElement("project_name")]
        public string? project_name { get; set; }

        [BsonElement("service_name")]
        public string? service_name { get; set; }

        [BsonElement("log_type")]
        public string? log_type { get; set; }

        [BsonElement("entity_type")]
        public string? entity_type { get; set; }

        [BsonElement("entity_id")]
        public string? entity_id { get; set; }

        [BsonElement("entity_rand_id")]
        public string? entity_rand_id { get; set; }

        [BsonElement("title")]
        public string? title { get; set; }

        [BsonElement("message")]
        public string? message { get; set; }

        [BsonElement("created_by")]
        public string? created_by { get; set; }

        [BsonElement("datetime")]
        public DateTime? datetime { get; set; }

        [BsonElement("ip")]
        public string? ip { get; set; }

        [BsonElement("device_info")]
        public string? device_info { get; set; }

        [BsonElement("request")]
        public string? request { get; set; }

        [BsonElement("response")]
        public string? response { get; set; }

        [BsonElement("status")]
        public string? status { get; set; }
    }
}
