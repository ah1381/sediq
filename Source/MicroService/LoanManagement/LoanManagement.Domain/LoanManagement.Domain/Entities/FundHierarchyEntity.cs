using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Domain.Entities
{
    public class FundHierarchyEntity : BaseEntity
    {
        public long? FundID { get; set; } // Changed to long? to match FundEntity.RowId
        public long? ParentFundID { get; set; } // Changed to long?
        public string Description { get; set; } = string.Empty;
        public FundEntity Fund { get; set; } = null!;
        public FundEntity? ParentFund { get; set; } // Nullable navigation property
    }
}
