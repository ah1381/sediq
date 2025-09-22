namespace Raya.Hrm.Shared.Library.Models.Base
{
    public record PackResponseBase
    {
        public long? PackRowId { get; set; }
        public string PackRandId { get; set; }
        public DateTime? PackCreatedAt { get; set; }
        public DateTime? PackUpdatedAt { get; set; }
        public short? PackRevisionSequence { get; set; }
        public long? PackPrRowId { get; set; } // Nullable because pr_row_id can be NULL
        public string PackPrRandId { get; set; }
        public short? PackStatus { get; set; }
        public Dictionary<string, object> PackMeta { get; set; } // JSONB stored as string
        public string[] PackPath { get; set; } // PostgreSQL text[] mapped to C# string[]
        public Dictionary<string, object> PackUnits { get; set; } // JSONB stored as string
        public Dictionary<string, object> PackSettings { get; set; } // JSONB stored as string
        public Dictionary<string, object> PackAccessControl { get; set; } // JSONB stored as string
    }
}
