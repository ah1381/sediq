using Raya.Hrm.Shared.Library.ModelS;

namespace LoanManagement.Domain.Entities
{
    public class FundElectionEntity : BaseEntity
    {
        public long FundID { get; set; } // Fund
        public DateTime? ElectionDate { get; set; } // Election date
        public long CandidateID { get; set; } // Candidate (references Personnel)
        public int Votes { get; set; } // Number of votes
        public string Position { get; set; } = string.Empty; // Position (elected member 1/2/...)
        public string FundStatus { get; set; } = string.Empty; // Status (held/in progress)
        public string CreatedBy { get; set; } = string.Empty; // Creator
        public FundEntity Fund { get; set; } = null!; // Navigation property for FundID
        public PersonnelEntity Candidate { get; set; } = null!; // Navigation property for CandidateID
    }
}
