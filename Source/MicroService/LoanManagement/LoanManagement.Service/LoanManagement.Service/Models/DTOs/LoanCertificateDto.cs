using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Service.Models.DTOs
{
    public class LoanCertificateDto : BaseEntity
    {
        public long LoanRequestId { get; set; }
        public string CertificateNumber { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; }
        public string Description { get; set; } = string.Empty;
    }

}
