using Common.Enums;

namespace RegistrationAPI.Models
{
    public class CreateUserApiModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ParrentUserName { get; set; }
    }
}