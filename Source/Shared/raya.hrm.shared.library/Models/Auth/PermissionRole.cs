namespace Raya.Hrm.Shared.Library.Models.Auth
{
    public class PermissionRole
    {
        public long RowId { get; set; }
        public long PermissionID { get; set; }
        public long RoleID { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }
}
