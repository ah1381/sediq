using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Domain.Entities
{
    public class LoanGuarantorEntity : BaseEntity
    {
        public long LoanRequestID { get; set; } // Loan request
        public long PersonnelID { get; set; } // Guarantor (personnel)
        public string GuarantorCode { get; set; } = string.Empty; // Guarantor employment code
        public DateTime? GuaranteeDate { get; set; } // Guarantee date
        public string LoanGuarantorStatus { get; set; } = string.Empty; // Guarantee status
        public string CreatedBy { get; set; } = string.Empty; // Creator
        public LoanRequestEntity LoanRequest { get; set; } = null!; // Navigation property for LoanRequestID
        public PersonnelEntity Personnel { get; set; } = null!; // Navigation property for PersonnelID
    }
}
