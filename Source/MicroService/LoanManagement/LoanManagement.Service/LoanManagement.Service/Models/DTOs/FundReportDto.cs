using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Service.Models.DTOs
{
    public class FundReportDto : BaseEntity
    {
        public long FundId { get; set; }
        public int ReportYear { get; set; }
        public string ReportType { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public string Description { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public bool Approved { get; set; }
    }
}
