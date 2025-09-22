namespace Raya.Hrm.Shared.Library.Models.Base
{
    public record PersonsWithPagingRequestBase : BaseRequestModel
    {
        public long? PersonRowId { get; set; }
        public DateTime? PersonCreatedAt { get; set; }
        public DateTime? PersonUpdatedAt { get; set; }
        public string? PersonNationalName { get; set; }
        public string? PersonNationalCode { get; set; }
        public string? PersonFirstName { get; set; }
        public string? PersonLastName { get; set; }
        public string? PersonFatherName { get; set; }
        public string? PersonSource { get; set; }
        public string? PersonAltRandId { get; set; }
        public short? PersonStatus { get; set; }
    }
}
