using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Domain.Entities
{
    public class FundTransferEntity : BaseEntity
    {
        public long MemberID { get; set; } // Member
        public long FromFundID { get; set; } // Source fund
        public long ToFundID { get; set; } // Destination fund
        public DateTime? TransferDate { get; set; } // Transfer date
        public string TransferType { get; set; } = string.Empty; // Type (debt/capital/membership)
        public decimal Amount { get; set; } // Transfer amount
        public string Description { get; set; } = string.Empty; // Description
        public string CreatedBy { get; set; } = string.Empty; // Creator
        public FundMemberEntity Member { get; set; } = null!; // Navigation property for MemberID
        public FundEntity FromFund { get; set; } = null!; // Navigation property for FromFundID
        public FundEntity ToFund { get; set; } = null!; // Navigation property for ToFundID
    }
}
