using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceModels.User
{
    public class UpdateUserModel
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
