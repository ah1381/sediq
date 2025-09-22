using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Domain.Entities
{
    public class FundInsuranceEntity : BaseEntity
    {
        public long FundID { get; set; } // Fund
        public long? LoanRequestID { get; set; } // Loan request (optional)
        public decimal InsuranceAmount { get; set; } // Insurance amount
        public DateTime? InsuranceDate { get; set; } // Insurance date
        public string Provider { get; set; } = string.Empty; // Insurance provider
        public string CreatedBy { get; set; } = string.Empty; // Creator
        public FundEntity Fund { get; set; } = null!; // Navigation property for FundID
        public LoanRequestEntity? LoanRequest { get; set; } // Navigation property for LoanRequestID (nullable)
    }
}
