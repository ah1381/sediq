using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Domain.Entities
{
    public class FundTransactionEntity : BaseEntity
    {
        public long FundID { get; set; } // Fund
        public long MemberID { get; set; } // Member
        public DateTime TransactionDate { get; set; } // Transaction date
        public string TransactionType { get; set; } = string.Empty; // Transaction type (capital increase/loan payment/installment deduction)
        public decimal Amount { get; set; } // Amount
        public decimal Balance { get; set; } // Balance
        public string Description { get; set; } = string.Empty; // Description
        public string CreatedBy { get; set; } = string.Empty; // Creator
        public decimal DebitAmount { get; set; } // Debit amount
        public decimal CreditAmount { get; set; } // Credit amount
        public decimal Commission { get; set; } // Commission
        public decimal Insurance { get; set; } // Insurance
        public FundEntity Fund { get; set; } = null!; // Navigation property for FundID
        public FundMemberEntity Member { get; set; } = null!; // Navigation property for MemberID
    }
}
