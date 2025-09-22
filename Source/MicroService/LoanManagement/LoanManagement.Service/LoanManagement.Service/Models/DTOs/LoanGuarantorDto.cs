using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Service.Models.DTOs
{
    public class LoanGuarantorDto : BaseEntity
    {
        public long LoanRequestId { get; set; }
        public long PersonnelId { get; set; }
        public string GuarantorCode { get; set; } = string.Empty;
        public DateTime? GuaranteeDate { get; set; }
        public string LoanGuarantorStatus { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
    }
}
