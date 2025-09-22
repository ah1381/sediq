using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Service.Models.DTOs
{
    public class PersonnelDto : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string NationalCode { get; set; } = string.Empty;
        public string EmploymentCode { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FatherName { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string EmploymentType { get; set; } = string.Empty;
    }
}
