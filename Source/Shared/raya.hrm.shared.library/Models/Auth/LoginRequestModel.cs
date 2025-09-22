using Raya.Hrm.Shared.Library.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Hrm.Shared.Library.Models.Auth
{
    public record LoginRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        //public UserType UserType { get; set; }


    }
}
