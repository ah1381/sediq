using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Hrm.Shared.Library.Models
{
    [Table("error_logs", Schema = "error")]
    public class ErrorModel
    {
        [Key]
        [Column("RowId")]
        public int RowId { get; set; }

        [Column("request_id")]
        public Guid? RequestId { get; set; }

        [Column("app_name")]
        public string? AppName { get; set; }

        [Column("message")]
        public string? Message { get; set; }

        [Column("stack_trace")]
        public string? StackTrace { get; set; }

        [Column("inner_exception")]
        public string? InnerException { get; set; }

        [Column("ip")]
        public string? Ip { get; set; }

        [Column("project_name")]
        public string? ProjectName { get; set; }

        [Column("service_name")]
        public string? ServiceName { get; set; }

        [Column("log_type")]
        public string? LogType { get; set; }

        [Column("entity_type")]
        public string? EntityType { get; set; }

        [Column("entity_id")]
        public string? EntityId { get; set; }

        [Column("entity_rand_id")]
        public string? EntityRandId { get; set; }

        [Column("title")]
        public string? Title { get; set; }

        [Column("created_by")]
        public string? CreatedBy { get; set; }

        [Column("datetime")]
        public DateTime? DateTime { get; set; }

        [Column("device_info")]
        public string? DeviceInfo { get; set; }

        [Column("request")]
        public string? Request { get; set; }

        [Column("response")]
        public string? Response { get; set; }

        [Column("status")]
        public string? Status { get; set; }

        // not mapped props
        [NotMapped] public Exception? Ex { get; set; }
        [NotMapped] public string? ConnectionType { get; set; }
    }


}
