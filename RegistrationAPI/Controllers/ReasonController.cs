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
        [Authorize(Policy = "Registration")]
        public async Task<ReasonResponseModel> GetReasons([FromQuery] ReasonRequestModel request) =>await _reasonService.GetReasons(request);
        [HttpPost("create_reason")]
        [Authorize(Policy = "Registration")]
        public BaseResponseModel CreateReason([FromBody] CreateEditReasonModel request) => _reasonService.CreateReason(request);
        [HttpPatch("edit_reason")]
        [Authorize(Policy = "Registration")]
        public BaseResponseModel EditReason([FromBody] CreateEditReasonModel request) => _reasonService.EditReason(request);
        [HttpDelete("delete_reason")]
        [Authorize(Policy = "Registration")]
        public BaseResponseModel DeleteReason([FromBody] DeleteReasonModel request) => _reasonService.DeleteReason(request);
    }
}
