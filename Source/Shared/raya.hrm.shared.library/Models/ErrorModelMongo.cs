using Raya.Hrm.Shared.Library.Models.Exception;

namespace Raya.Hrm.Shared.Library.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System;

    namespace Raya.Hrm.Shared.Library.Models
    {
        public class ErrorModelMongo
        {
            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public string? Id { get; set; }

            [BsonElement("request_id")]
            public Guid? RequestId { get; set; }

            [BsonElement("app_name")]
            public string? AppName { get; set; }

            [BsonElement("message")]
            public string? Message { get; set; }

            [BsonElement("stack_trace")]
            public string? StackTrace { get; set; }

            [BsonElement("inner_exception")]
            public string? InnerException { get; set; }

            [BsonElement("ip")]
            public string? Ip { get; set; }

            [BsonElement("project_name")]
            public string? ProjectName { get; set; }

            [BsonElement("service_name")]
            public string? ServiceName { get; set; }

            [BsonElement("log_type")]
            public string? LogType { get; set; }

            [BsonElement("entity_type")]
            public string? EntityType { get; set; }

            [BsonElement("entity_id")]
            public string? EntityId { get; set; }

            [BsonElement("entity_rand_id")]
            public string? EntityRandId { get; set; }

            [BsonElement("title")]
            public string? Title { get; set; }

            [BsonElement("created_by")]
            public string? CreatedBy { get; set; }

            [BsonElement("datetime")]
            public DateTime? DateTime { get; set; }

            [BsonElement("device_info")]
            public string? DeviceInfo { get; set; }

            [BsonElement("request")]
            public string? Request { get; set; }

            [BsonElement("response")]
            public string? Response { get; set; }

            [BsonElement("status")]
            public string? Status { get; set; }

            [BsonIgnore]
            public BpcValidationException? Ex { get; set; }

            [BsonIgnore]
            public string? ConnectionType { get; set; }
        }
    }
}