using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Hrm.Shared.Library.Models.Auth
{
    public class CreateUserRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }

    }
    public class CreateUserInfo : CreateUserRequest
    {
        public string UserId { get; set; }
        public string CurrentUser { get; set; }
    }
}
