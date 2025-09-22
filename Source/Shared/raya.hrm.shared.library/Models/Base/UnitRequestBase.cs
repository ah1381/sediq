namespace Raya.Hrm.Shared.Library.Models.Base
{
    public record UnitRequestBase
    {
        public long? UnitRowId { get; set; }
        public string UnitRandId { get; set; }
        public DateTime? UnitCreatedAt { get; set; }
        public DateTime? UnitUpdatedAt { get; set; }
        public long? UnitPrRowId { get; set; } // Nullable because pr_row_id can be NULL
        public string UnitPrRandId { get; set; }
        public short? UnitStatus { get; set; }
        //public string Content { get; set; } // JSONB stored as string
        //public string Meta { get; set; } // JSONB stored as string
        //public string[] Path { get; set; } // PostgreSQL text[] to C# string[]
        public string UnitVersion { get; set; }
    }
    public record UnitPersonRequestBase : PersonsRequestBase
    {
        public long? UnitRowId { get; set; }
        public string UnitRandId { get; set; }
        public DateTime? UnitCreatedAt { get; set; }
        public DateTime? UnitUpdatedAt { get; set; }
        public long? UnitPrRowId { get; set; } // Nullable because pr_row_id can be NULL
        public string UnitPrRandId { get; set; }
        public short? UnitStatus { get; set; }
        //public string Content { get; set; } // JSONB stored as string
        //public string Meta { get; set; } // JSONB stored as string
        //public string[] Path { get; set; } // PostgreSQL text[] to C# string[]
        public string UnitVersion { get; set; }
    }
}
