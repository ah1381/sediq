using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Service.Models.DTOs
{
    public class FundRegionDto : BaseEntity
    {
        public long FundId { get; set; }
        public string RegionName { get; set; } = string.Empty;
        public int ActiveMembers { get; set; }
        public int RetiredMembers { get; set; }
        public string BankName { get; set; } = string.Empty;
        public string BranchCode { get; set; } = string.Empty;
        public string OrgCode { get; set; } = string.Empty;
        public List<string> InstallmentDeductionCodes { get; set; } = new List<string>();
        public List<string> ShareDeductionCodes { get; set; } = new List<string>();
    }
}
