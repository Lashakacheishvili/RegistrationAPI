using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegistrationAPI.Models;
using Service.ServiceInterfaces;
using ServiceModels;
using ServiceModels.User;

namespace RegistrationAPI.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("create_user")]
        [Authorize(Policy = "Registration")]
        public BaseResponseModel CreateUser([FromBody] CreateUserApiModel request) => _userService.CreateUser(new CreateUserModel { UserName = request.UserName, PasswordHash = request.Password, ParrentUserName = request.ParrentUserName, UserRole = string.IsNullOrEmpty(request.ParrentUserName) ? UserRole.Parrent : UserRole.Child });
    }
}
