using ServiceModels;
using ServiceModels.Reason;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces
{
    public interface IReasonService
    {
        BaseResponseModel CreateReason(CreateEditReasonModel model,int? userId);
        BaseResponseModel EditReason(CreateEditReasonModel model, int? userId);
        BaseResponseModel DeleteReason(DeleteReasonModel model, int? userId);
        Task<ReasonResponseModel> GetReasons(ReasonRequestModel request, int? userId);
    }
}
