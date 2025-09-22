namespace Raya.Hrm.Shared.Library.Models.Base
{
    public record PackRequestBase : BaseQueryRequest
    {
        public long? PackRowId { get; set; }
        public string PackRandId { get; set; }
        public DateTime? PackCreatedAt { get; set; }
        public DateTime? PackUpdatedAt { get; set; }
        public long? PackPrRowId { get; set; } // Nullable because pr_row_id can be NULL
        public string PackPrRandId { get; set; }
        public short? PackStatus { get; set; }
    }
}
