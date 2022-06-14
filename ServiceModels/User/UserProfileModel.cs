using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceModels.User
{
    public class UserProfileModel:UpdateUserModel
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public string UserName { get; set; }
    }
}
