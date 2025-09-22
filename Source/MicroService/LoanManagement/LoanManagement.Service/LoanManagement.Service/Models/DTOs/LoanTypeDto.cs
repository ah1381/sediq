using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Service.Models.DTOs
{
    public class LoanTypeDto : BaseEntity
    {
        public long FundId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal MaxAmount { get; set; }
        public int MinInstallments { get; set; }
        public int MaxInstallments { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }
}
