using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Domain.Entities
{
    public class LoanAdjustmentRequestEntity : BaseEntity
    {
        public long MemberID { get; set; } // Member
        public string RequestType { get; set; } = string.Empty; // Request type (installment change/refunded/share increase)
        public decimal OldValue { get; set; } // Old value
        public decimal NewValue { get; set; } // New value
        public decimal MaxAllowed { get; set; } // Maximum allowed
        public string Description { get; set; } = string.Empty; // Description
        public DateTime RequestDate { get; set; } = DateTime.Now; // Request date
        public string StatusDesc { get; set; } = string.Empty; // Status (registered/approved/rejected)
        public string CreatedBy { get; set; } = string.Empty; // Creator
        public bool Online { get; set; } // Registered online
        public FundMemberEntity Member { get; set; } = null!; // Navigation property for MemberID
    }
}
