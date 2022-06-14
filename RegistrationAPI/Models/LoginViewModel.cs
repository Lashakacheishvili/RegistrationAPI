namespace RegistrationAPI.Models
{
    public class LoginResponseModel
    {
        public string AccessToken { get; set; }
        public bool Success { get; set; }
        public string UserMessage { get; set; }
    }
    public class LoginRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
