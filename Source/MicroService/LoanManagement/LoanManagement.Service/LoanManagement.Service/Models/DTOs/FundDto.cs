using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Service.Models.DTOs
{
    public class FundDto : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string FundType { get; set; } = string.Empty;
        public string CalculationType { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
    }
}
