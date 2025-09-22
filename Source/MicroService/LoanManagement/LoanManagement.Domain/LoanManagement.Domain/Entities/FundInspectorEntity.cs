using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Domain.Entities
{
    public class FundInspectorEntity : BaseEntity
    {
        public long FundID { get; set; } // Fund
        public long PersonnelID { get; set; } // Personnel
        public DateTime? StartDate { get; set; } // Start date
        public DateTime? EndDate { get; set; } // End date
        public string ReportFrequency { get; set; } = string.Empty; // Report frequency (e.g., every six months)
        public string CreatedBy { get; set; } = string.Empty; // Creator
        public FundEntity Fund { get; set; } = null!; // Navigation property for FundID
        public PersonnelEntity Personnel { get; set; } = null!; // Navigation property for PersonnelID
    }
}
