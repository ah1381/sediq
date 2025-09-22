using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Domain.Entities
{
    public class PersonnelEntity : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty; // First name
        public string LastName { get; set; } = string.Empty; // Last name
        public string NationalCode { get; set; } = string.Empty; // National code
        public string EmploymentCode { get; set; } = string.Empty; // Employment code
        public DateTime? BirthDate { get; set; } // Birth date
        public string Phone { get; set; } = string.Empty; // Phone
        public string Mobile { get; set; } = string.Empty; // Mobile
        public string Address { get; set; } = string.Empty; // Address
        public string Email { get; set; } = string.Empty; // Email
        public string FatherName { get; set; } = string.Empty; // Father's name
        public string Department { get; set; } = string.Empty; // Department
        public string Province { get; set; } = string.Empty; // Province
        public string EmploymentType { get; set; } = string.Empty; // Employment type (formal/contractual/retired)
    }
}
