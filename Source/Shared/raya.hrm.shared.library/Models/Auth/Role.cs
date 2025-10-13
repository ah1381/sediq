namespace Raya.Hrm.Shared.Library.Models.Auth
{
    public class Role
    {
        public long RowId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Permission> Permissions { get; set; }
    }
}
