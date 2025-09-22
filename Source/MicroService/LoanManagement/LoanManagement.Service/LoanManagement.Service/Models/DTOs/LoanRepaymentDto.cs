using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Service.Models.DTOs
{
    public class LoanRepaymentDto : BaseEntity
    {
        public long LoanRequestId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string ReceiptNumber { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public string PaymentGatewayRef { get; set; } = string.Empty;
    }
}
