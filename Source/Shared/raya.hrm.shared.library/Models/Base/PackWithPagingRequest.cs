namespace Raya.Hrm.Shared.Library.Models.Base
{
    public record PackWithPagingRequestBase : BaseRequestModel
    {
        public long? PackRowId { get; set; }
        public string PackRandId { get; set; }
        public DateTime? PackCreatedAt { get; set; }
        public DateTime? PackUpdatedAt { get; set; }
        public long? PackPrRowId { get; set; } // Nullable because pr_row_id can be NULL
        public string PackPrRandId { get; set; }
        public short? PackStatus { get; set; }
        //public Dictionary<string, object> Meta { get; set; } // JSONB stored as string
        //public string[] Path { get; set; } // PostgreSQL text[] mapped to C# string[]
        //public Dictionary<string, object> Packs { get; set; } // JSONB stored as string
        //public Dictionary<string, object> Settings { get; set; } // JSONB stored as string
        //public Dictionary<string, object> AccessControl { get; set; } // JSONB stored as string
    }
}
