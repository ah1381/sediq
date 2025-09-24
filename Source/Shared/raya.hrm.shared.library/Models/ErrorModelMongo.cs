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

            [BsonElement("RequestId")]
            public Guid? RequestId { get; set; }

            [BsonElement("Message")]
            public string? Message { get; set; }

            [BsonElement("StackTrace")]
            public string? StackTrace { get; set; }

            [BsonElement("InnerException")]
            public string? InnerException { get; set; }

            [BsonElement("AppName")]
            public string? AppName { get; set; }

            [BsonElement("Ip")]
            public string? Ip { get; set; }

            [BsonElement("ProjectName")]
            public string? ProjectName { get; set; }

            [BsonElement("ServiceName")]
            public string? ServiceName { get; set; }

            [BsonElement("LogType")]
            public string? LogType { get; set; }

            [BsonElement("EntityType")]
            public string? EntityType { get; set; }

            [BsonElement("EntityId")]
            public string? EntityId { get; set; }

            [BsonElement("EntityRandId")]
            public string? EntityRandId { get; set; }

            [BsonElement("Title")]
            public string? Title { get; set; }

            [BsonElement("CreatedBy")]
            public string? CreatedBy { get; set; }

            [BsonElement("DateTime")]
            public DateTime? DateTime { get; set; }

            [BsonElement("DeviceInfo")]
            public string? DeviceInfo { get; set; }

            [BsonElement("Request")]
            public string? Request { get; set; }

            [BsonElement("Response")]
            public string? Response { get; set; }

            [BsonElement("Status")]
            public string? Status { get; set; }

            // Not stored in Mongo
            [BsonIgnore]
            public Exception? Ex { get; set; }

            [BsonIgnore]
            public string? ConnectionType { get; set; }
        }
    }
}