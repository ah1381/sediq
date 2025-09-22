using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Service.Models.DTOs
{
    public class LoanAdjustmentRequestDto : BaseEntity
    {
        public long MemberId { get; set; }
        public string RequestType { get; set; } = string.Empty;
        public decimal OldValue { get; set; }
        public decimal NewValue { get; set; }
        public decimal MaxAllowed { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime RequestDate { get; set; }
        public string StatusDesc { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public bool Online { get; set; }
    }
}
