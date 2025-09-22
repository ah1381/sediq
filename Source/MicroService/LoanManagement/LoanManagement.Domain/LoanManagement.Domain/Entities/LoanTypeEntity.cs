using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Domain.Entities
{
    public class LoanTypeEntity : BaseEntity
    {
        public long FundID { get; set; } // Fund
        public string Name { get; set; } = string.Empty; // Loan type name
        public string Description { get; set; } = string.Empty; // Description
        public decimal MaxAmount { get; set; } // Maximum amount
        public int MinInstallments { get; set; } // Minimum installments
        public int MaxInstallments { get; set; } // Maximum installments
        public DateTime? StartDate { get; set; } // Allocation start date
        public DateTime? EndDate { get; set; } // Deactivation date
        public string CreatedBy { get; set; } = string.Empty; // Creator
        public FundEntity Fund { get; set; } = null!; // Navigation property for FundID
    }
}
