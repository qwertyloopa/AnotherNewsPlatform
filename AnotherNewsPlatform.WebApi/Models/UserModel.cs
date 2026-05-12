using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherNewsPlatform.WebApi.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public long RoleId { get; set; }
    }
}
