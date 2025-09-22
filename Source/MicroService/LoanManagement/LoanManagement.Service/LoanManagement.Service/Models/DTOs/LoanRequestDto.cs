using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Service.Models.DTOs
{
    public class LoanRequestDto : BaseEntity
    {
        public long PersonnelId { get; set; }
        public long LoanTypeId { get; set; }
        public long FundId { get; set; }
        public DateTime RequestDate { get; set; }
        public decimal AmountRequested { get; set; }
        public decimal MaxLoanAmount { get; set; }
        public int WaitingMonths { get; set; }
        public string StatusDesc { get; set; } = string.Empty;
        public int Installments { get; set; }
        public decimal MonthlyInstallment { get; set; }
        public string BankAccount { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public bool RequiresGuarantor { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string EmploymentType { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public bool Online { get; set; }
    }
}
