using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Domain.Entities
{
    public class NotificationEntity : BaseEntity
    {
        public long? PersonnelID { get; set; } // Changed to long? to match PersonnelEntity.RowId
        public string NotificationType { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime NotificationDate { get; set; }
        public string Channel { get; set; } = string.Empty;
        public bool Read { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public short? NotificationStatus { get; set; }
        public PersonnelEntity Personnel { get; set; } = null!;
    }
}
