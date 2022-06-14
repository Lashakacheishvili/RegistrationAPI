using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.ServiceInterfaces;
using ServiceModels;
using ServiceModels.Reason;
using System.Threading.Tasks;

namespace RegistrationAPI.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ReasonController : BaseController
    {
        private readonly IReasonService _reasonService;
        public ReasonController(IReasonService reasonService)
        {
            _reasonService=reasonService;
        }
        [HttpGet("reasons")]
        [Authorize(Policy = "RegistrationUser")]
        public async Task<ReasonResponseModel> GetReasons([FromQuery] ReasonRequestModel request) =>await _reasonService.GetReasons(request,UserId);
        [HttpPost("create_reason")]
        [Authorize(Policy = "RegistrationUser")]
        public BaseResponseModel CreateReason([FromBody] CreateEditReasonModel request)=> _reasonService.CreateReason(request,UserId);
        [HttpPatch("edit_reason")]
        [Authorize(Policy = "RegistrationUser")]
        public BaseResponseModel EditReason([FromBody] CreateEditReasonModel request) => _reasonService.EditReason(request, UserId);
        [HttpDelete("delete_reason")]
        [Authorize(Policy = "RegistrationUser")]
        public BaseResponseModel DeleteReason([FromBody] DeleteReasonModel request) => _reasonService.DeleteReason(request, UserId);
    }
}
