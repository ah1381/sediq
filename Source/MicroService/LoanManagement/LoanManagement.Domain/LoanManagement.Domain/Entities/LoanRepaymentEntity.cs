using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Domain.Entities
{
    public class LoanRepaymentEntity : BaseEntity
    {
        public long LoanRequestID { get; set; } // Loan request
        public DateTime PaymentDate { get; set; } // Payment date
        public decimal Amount { get; set; } // Amount
        public decimal Balance { get; set; } // Balance
        public string ReceiptNumber { get; set; } = string.Empty; // Receipt number
        public string CreatedBy { get; set; } = string.Empty; // Creator
        public string PaymentMethod { get; set; } = string.Empty; // Payment method (electronic/salary deduction)
        public string PaymentGatewayRef { get; set; } = string.Empty; // Payment gateway reference
        public LoanRequestEntity LoanRequest { get; set; } = null!; // Navigation property for LoanRequestID
    }
}
