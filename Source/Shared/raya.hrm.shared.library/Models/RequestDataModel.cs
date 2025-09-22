using System.ComponentModel.DataAnnotations.Schema;

namespace Raya.Hrm.Shared.Library.Models
{
    public class RequestDataModel
    {
        public Guid request_id { get; set; } = Guid.NewGuid();
        public DateTime cr { get; set; } = DateTime.UtcNow;

        public string requesttype { get; set; } = null!;
        public string Path { get; set; } = null!;
        public string? queryString { get; set; }

        public string headers { get; set; } = null!;
        public string? body { get; set; }

        public string? clientIp { get; set; }
        public string? userAgent { get; set; }
        public string appName { get; set; } = null!;

        [NotMapped]
        public string? ConnectionType { get; set; }

        // 🔹 New fields
        public string? project_name { get; set; }
        public string? service_name { get; set; }
        public string? log_type { get; set; }
        public string? entity_type { get; set; }
        public string? entity_id { get; set; }
        public string? entity_rand_id { get; set; }
        public string? title { get; set; }
        public string? message { get; set; }
        public string? created_by { get; set; }
        public DateTime? datetime { get; set; }
        public string? ip { get; set; }
        public string? device_info { get; set; }
        public string? request { get; set; }
        public string? response { get; set; }
        public string? status { get; set; }
    }

}
