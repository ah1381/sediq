using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Service.Models.DTOs
{
    public class FundHierarchyDto : BaseEntity
    {
        public long FundId { get; set; }
        public long? ParentFundId { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
