using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Service.Models.DTOs
{
    public class FundInspectorDto : BaseEntity
    {
        public long FundId { get; set; }
        public long PersonnelId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ReportFrequency { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
    }
}
