using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Domain.Entities
{
    public class FundReportEntity : BaseEntity
    {
        public long FundID { get; set; } // Fund
        public int ReportYear { get; set; } // Report year
        public string ReportType { get; set; } = string.Empty; // Type (four-column balance/annual balance)
        public decimal Balance { get; set; } // Balance
        public string Description { get; set; } = string.Empty; // Description
        public string CreatedBy { get; set; } = string.Empty; // Creator
        public bool Approved { get; set; } // Approved
        public FundEntity Fund { get; set; } = null!; // Navigation property for FundID
    }
}
