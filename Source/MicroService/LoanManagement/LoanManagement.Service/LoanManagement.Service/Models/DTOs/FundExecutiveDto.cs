using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Service.Models.DTOs
{
    public class FundExecutiveDto : BaseEntity
    {
        public long FundId { get; set; }
        public long PersonnelId { get; set; }
        public string Role { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool HasSignPermission { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string OrderNumber { get; set; } = string.Empty;
        public bool Elected { get; set; }
    }
}
