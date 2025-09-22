using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Service.Models.DTOs
{
    public class FundTransferDto : BaseEntity
    {
        public long MemberId { get; set; }
        public long FromFundId { get; set; }
        public long ToFundId { get; set; }
        public DateTime? TransferDate { get; set; }
        public string TransferType { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
    }
}
