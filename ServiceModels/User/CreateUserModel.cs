using Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceModels.User
{
    public class CreateUserModel
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp => Guid.NewGuid().ToString();
        public string ConcurrencyStamp => Guid.NewGuid().ToString();
        public bool EmailConfirmed { get; set; } = true;
        public bool PhoneNumberConfirmed { get; set; } = true;
        public bool TwoFactorEnabled { get; set; } = false;
        public bool LockoutEnabled { get; set; } = true;
        public int AccessFailedCount { get; set; }
        public UserStatus UserStatus { get; set; }
        public UserRole UserRole { get; set; }
        public string ParrentUserName { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now.ToUniversalTime().AddHours(4);
    }
}
