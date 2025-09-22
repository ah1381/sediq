using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Domain.Entities
{
    public class FundMemberEntity : BaseEntity
    {
        public long? PersonnelID { get; set; } // Changed to long?
        public long? FundID { get; set; } // Changed to long?
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
        public PersonnelEntity Personnel { get; set; } = null!;
        public FundEntity Fund { get; set; } = null!;
    }
}
