using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Service.Models.DTOs
{
    public class NotificationDto : BaseEntity
    {
        public long? RowId { get; set; } // Changed to long? to match BaseEntity
        public string RandId { get; set; } = string.Empty;
        public long? PersonnelId { get; set; } // Changed to long? to match NotificationEntity
        public string NotificationType { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime NotificationDate { get; set; }
        public string Channel { get; set; } = string.Empty;
        public bool Read { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public short? NotificationStatus { get; set; }
    }
}
