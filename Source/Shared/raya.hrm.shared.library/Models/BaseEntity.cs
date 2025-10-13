namespace Raya.Hrm.Shared.Library.ModelS
{
    public abstract class BaseEntity
    {
        public long RowId { get; set; }
        public string RandId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public short RevSeq { get; set; }
        public short Status { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }
}
