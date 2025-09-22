namespace Raya.Hrm.Shared.Library.Models.Base
{
    public record UnitResponseBase
    {
        public long UnitRowId { get; set; }
        public string UnitRandId { get; set; }
        public DateTime UnitCreatedAt { get; set; }
        public DateTime UnitUpdatedAt { get; set; }
        public long? UnitParentRowId { get; set; } // Nullable because pr_row_id can be NULL
        public string UnitParentRandId { get; set; }
        public short UnitStatus { get; set; }
        public Dictionary<string, object> UnitContent { get; set; } // JSONB stored as string
        public Dictionary<string, object> UnitMeta { get; set; } // JSONB stored as string
        public string[] UnitPath { get; set; } // PostgreSQL text[] to C# string[]
        public string UnitVersion { get; set; }
        public short UnitRevisionSequence { get; set; }
    }
}
