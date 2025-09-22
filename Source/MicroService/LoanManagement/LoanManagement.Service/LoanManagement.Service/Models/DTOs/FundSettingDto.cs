using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Service.Models.DTOs
{
    public class FundSettingDto : BaseEntity
    {
        public long FundId { get; set; }
        public decimal MaxLoanAmount { get; set; }
        public decimal LoanMultiplier { get; set; }
        public decimal CommissionPercent { get; set; }
        public decimal MinShareAmount { get; set; }
        public int MinMembershipMonths { get; set; }
        public bool SingleLoanPerMember { get; set; }
        public bool MultiPaymentAllowed { get; set; }
        public int MaxInstallmentChange { get; set; }
        public int RepaymentMonths { get; set; }
        public decimal InsurancePercent { get; set; }
    }
}
