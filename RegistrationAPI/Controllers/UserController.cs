using Common.Enums;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RegistrationAPI.Models;
using Service.ServiceInterfaces;
using ServiceModels;
using ServiceModels.User;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RegistrationAPI.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration; 
        }
        [HttpGet("user_info")]
        [Authorize("RegistrationUser")]
        public UserProfileModel GetUser() => _userService.GetUser(UserId);
        [HttpGet("childs")]
        [Authorize("RegistrationUser")]
        public List<UserProfileModel> GetChilds() => _userService.GetChilds(UserId);
        [HttpPost("create_user")]
        [Authorize(Policy = "Registration")]
        public BaseResponseModel CreateUser([FromBody] CreateUserApiModel request) => _userService.CreateUser(new CreateUserModel { UserName = request.UserName, PasswordHash = request.Password, ParrentUserName = request.ParrentUserName, UserRole = string.IsNullOrEmpty(request.ParrentUserName) ? UserRole.Parrent : UserRole.Child });
        [HttpPost("login")]
        [Authorize(Policy = "Registration")]
        public async Task<LoginResponseModel> Login([FromBody] LoginRequestModel request)
        {

            if (!ModelState.IsValid)
            {
                return new LoginResponseModel { UserMessage = "იუზერი ან პაროლი ცარიელია " };
            }
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(_configuration.GetValue<string>("APIHost").TrimEnd('/') + "/");
            if (disco.IsError)
            {
                return new LoginResponseModel { UserMessage = "არასწორი მომხმარებელი" };
            }
            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest { Address = disco.TokenEndpoint, UserName = request.UserName, Password = request.Password, ClientId = "RegistrationUser" });

            if (tokenResponse.IsError)
            {
                return new LoginResponseModel { UserMessage = "Token არასწორია" };
            }
            return new LoginResponseModel { Success = true, AccessToken = tokenResponse.AccessToken };
        }
        [HttpPatch("edit_user")]
        [Authorize("RegistrationUser")]
        public BaseResponseModel UpdateUser([FromBody]UpdateUserModel model) => _userService.UpdateUser(model, UserId);
        [HttpPatch("change_password")]
        [Authorize("RegistrationUser")]
        public BaseResponseModel ChangePassword([FromBody]string password) => _userService.ChangePassword(password, UserId);
    }
}
