using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Service.Models.DTOs
{
    public class FundMemberDto : BaseEntity
    {
        public long PersonnelId { get; set; }
        public long FundId { get; set; }
        public string MembershipType { get; set; } = string.Empty;
        public string MembershipNumber { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Shares { get; set; }
        public List<string> ShareDeductionCodes { get; set; } = new List<string>();
        public string BankAccount { get; set; } = string.Empty;
        public string EmploymentType { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string StatusDesc { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public decimal TotalSalary { get; set; }
        public decimal NetPayment { get; set; }
        public string Province { get; set; } = string.Empty;
        public int WaitingMonths { get; set; }
    }
}
