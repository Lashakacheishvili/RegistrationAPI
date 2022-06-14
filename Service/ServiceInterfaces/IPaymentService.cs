using ServiceModels;
using ServiceModels.Payment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces
{
    public interface IPaymentService
    {
        BaseResponseModel CreatePayment(CreatePaymentModel model, int? userId);
        Task<PaymentResponseModel> GetPayments(PaymentRequestModel request, int? userId);
        BaseResponseModel CheckoutReason(int reasonId, int? userId);
    }
}
