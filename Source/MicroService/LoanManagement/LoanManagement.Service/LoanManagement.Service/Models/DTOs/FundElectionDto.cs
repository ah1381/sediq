using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Service.Models.DTOs
{
    public class FundElectionDto : BaseEntity
    {
        public long FundId { get; set; }
        public DateTime? ElectionDate { get; set; }
        public long CandidateId { get; set; }
        public int Votes { get; set; }
        public string Position { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
    }
}
