using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Domain.Entities
{
    public class FundRegionEntity : BaseEntity
    {
        public long FundID { get; set; } // Fund
        public string RegionName { get; set; } = string.Empty; // Region name
        public int ActiveMembers { get; set; } // Active members
        public int RetiredMembers { get; set; } // Retired members
        public string BankName { get; set; } = string.Empty; // Bank name
        public string BranchCode { get; set; } = string.Empty; // Branch code
        public string OrgCode { get; set; } = string.Empty; // Organization code
        public List<string> InstallmentDeductionCodes { get; set; } = new List<string>(); // Installment deduction codes
        public List<string> ShareDeductionCodes { get; set; } = new List<string>(); // Share deduction codes
        public FundEntity Fund { get; set; } = null!; // Navigation property for FundID
    }
}
