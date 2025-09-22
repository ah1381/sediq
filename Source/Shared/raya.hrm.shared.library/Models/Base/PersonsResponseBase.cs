namespace Raya.Hrm.Shared.Library.Models.Base
{
    public record PersonsResponseBase
    {
        public long? PersonRowId { get; set; }
        public string PersonRandId { get; set; }
        public DateTime PersonCreatedAt { get; set; }
        //public long PersonRevisionSequence { get; set; }
        public DateTime PersonUpdatedAt { get; set; }
        public string PersonNationalName { get; set; }
        public string PersonNationalCode { get; set; }
        public string PersonFirstName { get; set; }
        public string PersonLastName { get; set; }
        public string PersonFatherName { get; set; }
        public string PersonSource { get; set; }
        public string PersonAltRandId { get; set; }
        public short? PersonStatus { get; set; }
    }
    public record PersonsGeneralResponseBase
    {
        public long? RowId { get; set; }
        public string RandId { get; set; }
        public DateTime? CreatedAt { get; set; }
        //public long RevisionSequence { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string NationalName { get; set; }
        public string NationalCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Source { get; set; }
        public string AltRandId { get; set; }
        public short? Status { get; set; }
    }
}
