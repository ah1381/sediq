using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Domain.Entities
{
    public class LoanCertificateEntity : BaseEntity
    {
        public long LoanRequestID { get; set; } // Loan request
        public string CertificateNumber { get; set; } = string.Empty; // Certificate number
        public DateTime IssueDate { get; set; } // Issue date
        public string Description { get; set; } = string.Empty; // Description
        public LoanRequestEntity LoanRequest { get; set; } = null!; // Navigation property for LoanRequestID
    }
}
