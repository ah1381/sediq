namespace Raya.Hrm.Shared.Library.Models.Base
{
    public record OrganRequestBase : BaseQueryRequest
    {
        public string OrganName { get; set; }    // Name of the organ
        public string OrganLocalId { get; set; }
        //public DateTime? OrganCreatedAt { get; set; }
        //public DateTime? OrganUpdatedAt { get; set; }
        public short? OrganStatus { get; set; }
        public long? OrganRowId { get; set; }
    }
}
