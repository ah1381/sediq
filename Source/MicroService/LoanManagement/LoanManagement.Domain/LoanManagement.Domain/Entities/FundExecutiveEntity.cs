using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Domain.Entities
{
    public class FundExecutiveEntity : BaseEntity
    {
        public long FundID { get; set; } // Fund
        public long PersonnelID { get; set; } // Personnel
        public string Role { get; set; } = string.Empty; // Role (chairman/CEO/treasurer/inspector/elected member)
        public DateTime? StartDate { get; set; } // Start date
        public DateTime? EndDate { get; set; } // End date
        public bool HasSignPermission { get; set; } // Signature permission
        public string CreatedBy { get; set; } = string.Empty; // Creator
        public string OrderNumber { get; set; } = string.Empty; // Order number
        public bool Elected { get; set; } // Is elected
        public FundEntity Fund { get; set; } = null!; // Navigation property for FundID
        public PersonnelEntity Personnel { get; set; } = null!; // Navigation property for PersonnelID
    }
}
