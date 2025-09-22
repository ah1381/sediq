namespace Raya.Hrm.Shared.Library.Models.Base
{
    public record IrOrganResponseBase
    {
        public long IrOrganRowId { get; set; }   // rowId (primary key)
        //public string IrOrganRandId { get; set; }  // Unique ID for the organ
        public DateTime? IrOrganCreatedAt { get; set; }  // CreatedAt timestamp
        public short IrOrganRevisionSequence { get; set; }  // Revision sequence number
        public DateTime? IrOrganUpdatedOn { get; set; }  // Last updated timestamp
        public short? IrOrganStatus { get; set; }  // Status of the organ
        public string IrOrganName { get; set; }  // Name of the organ
        public string[] IrOrganPath { get; set; }  // Path (list of strings)
        public Dictionary<string, object> IrOrganMetadata { get; set; }  // Metadata in JSON format
        public Dictionary<string, object> IrOrganSida { get; set; }  // Sida data in JSON format
        public string IrOrganLocalId { get; set; }  // Local unique ID
        public Dictionary<string, object> IrOrganHrm { get; set; }  // HRM data in JSON format
        public Dictionary<string, object> IrOrganSida2 { get; set; }  // Additional Sida data in JSON format
    }
}
