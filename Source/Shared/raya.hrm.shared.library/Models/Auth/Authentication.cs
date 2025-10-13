namespace Raya.Hrm.Shared.Library.Models.Auth
{
    public class Authentication
    {
        public long RowId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        public string OwnerProject { get; set; }
        public long? UserType { get; set; }
        public List<Role> Roles { get; set; }
    }
}
