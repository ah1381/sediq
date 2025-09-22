using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Domain.Entities
{
    public class FundSettingEntity : BaseEntity
    {
        public long FundID { get; set; } // Fund
        public decimal MaxLoanAmount { get; set; } // Maximum loan amount
        public decimal LoanMultiplier { get; set; } // Loan multiplier
        public decimal CommissionPercent { get; set; } // Commission percentage (e.g., 1%)
        public decimal MinShareAmount { get; set; } // Minimum share amount
        public int MinMembershipMonths { get; set; } // Minimum membership months
        public bool SingleLoanPerMember { get; set; } // Only one loan per member
        public bool MultiPaymentAllowed { get; set; } // Multiple payments allowed
        public int MaxInstallmentChange { get; set; } = 1; // Maximum installment change
        public int RepaymentMonths { get; set; } = 36; // Repayment months (default 36)
        public decimal InsurancePercent { get; set; } // Insurance percentage
        public FundEntity Fund { get; set; } = null!; // Navigation property for FundID
    }
}
