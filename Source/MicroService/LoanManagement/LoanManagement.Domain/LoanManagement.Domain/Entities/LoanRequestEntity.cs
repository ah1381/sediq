using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Domain.Entities
{
    public class LoanRequestEntity : BaseEntity
    {
        public long PersonnelID { get; set; } // Personnel
        public long LoanTypeID { get; set; } // Loan type
        public long FundID { get; set; } // Fund
        public DateTime RequestDate { get; set; } // Request date
        public decimal AmountRequested { get; set; } // Requested amount
        public decimal MaxLoanAmount { get; set; } // Maximum loan amount
        public int WaitingMonths { get; set; } // Waiting months
        public string StatusDesc { get; set; } = string.Empty; // Status
        public int Installments { get; set; } // Number of installments
        public decimal MonthlyInstallment { get; set; } // Monthly installment
        public string BankAccount { get; set; } = string.Empty; // Bank account
        public string Address { get; set; } = string.Empty; // Address
        public string Phone { get; set; } = string.Empty; // Phone
        public string Mobile { get; set; } = string.Empty; // Mobile
        public bool RequiresGuarantor { get; set; } // Requires guarantor
        public string CreatedBy { get; set; } = string.Empty; // Creator
        public string Province { get; set; } = string.Empty; // Province
        public string Region { get; set; } = string.Empty; // Region
        public string EmploymentType { get; set; } = string.Empty; // Employment type
        public string Department { get; set; } = string.Empty; // Department
        public bool Online { get; set; } // Registered online
        public PersonnelEntity Personnel { get; set; } = null!; // Navigation property for PersonnelID
        public LoanTypeEntity LoanType { get; set; } = null!; // Navigation property for LoanTypeID
        public FundEntity Fund { get; set; } = null!; // Navigation property for FundID
    }
}
