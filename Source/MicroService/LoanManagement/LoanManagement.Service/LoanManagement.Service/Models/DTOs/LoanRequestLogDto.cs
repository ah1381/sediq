using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Service.Models.DTOs
{
    public class LoanRequestLogDto : BaseEntity
    {
        public long LoanRequestId { get; set; }
        public string Action { get; set; } = string.Empty;
        public string ActionBy { get; set; } = string.Empty;
        public DateTime ActionDate { get; set; }
        public string Note { get; set; } = string.Empty;
    }
}
