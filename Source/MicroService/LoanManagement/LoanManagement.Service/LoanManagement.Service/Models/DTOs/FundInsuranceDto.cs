using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Service.Models.DTOs
{
    public class FundInsuranceDto : BaseEntity
    {
        public long FundId { get; set; }
        public long? LoanRequestId { get; set; }
        public decimal InsuranceAmount { get; set; }
        public DateTime? InsuranceDate { get; set; }
        public string Provider { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
    }
}
