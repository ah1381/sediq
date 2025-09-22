using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Domain.Entities
{
    public class LoanRequestLogEntity : BaseEntity
    {
        public long LoanRequestID { get; set; } // Loan request
        public string Action { get; set; } = string.Empty; // Action (registered/approved/rejected/paid)
        public string ActionBy { get; set; } = string.Empty; // Performed by
        public DateTime ActionDate { get; set; } // Action date
        public string Note { get; set; } = string.Empty; // Note
        public LoanRequestEntity LoanRequest { get; set; } = null!; // Navigation property for LoanRequestID
    }
}
