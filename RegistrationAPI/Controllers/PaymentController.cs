using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.ServiceInterfaces;
using ServiceModels;
using ServiceModels.Payment;
using System.Threading.Tasks;

namespace RegistrationAPI.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpGet("payments")]
        [Authorize(Policy = "RegistrationUser")]
        public async Task<PaymentResponseModel> GetPayments([FromQuery] PaymentRequestModel request) => await _paymentService.GetPayments(request, UserId);
        [HttpPost("create_payment")]
        [Authorize("RegistrationUser")]
        public BaseResponseModel CreatePayment([FromBody] CreatePaymentModel model) => _paymentService.CreatePayment(model, UserId);
        [HttpPost("checkout_reason")]
        [Authorize("RegistrationUser")]
        public BaseResponseModel CheckoutReason([FromBody] int reasonId) => _paymentService.CheckoutReason(reasonId, UserId);
    }
}
