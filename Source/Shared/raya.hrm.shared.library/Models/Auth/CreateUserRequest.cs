namespace Raya.Hrm.Shared.Library.Models.Auth
{
    public class CreateUserRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Role> Roles { get; set; }

    }
    public class CreateUserInfo : CreateUserRequest
    {
        public string UserId { get; set; }
        public string CurrentUser { get; set; }
    }
}
