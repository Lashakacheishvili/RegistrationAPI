using ServiceModels;
using ServiceModels.Reason;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces
{
    public interface IReasonService
    {
        BaseResponseModel CreateReason(CreateEditReasonModel model);
        BaseResponseModel EditReason(CreateEditReasonModel model);
        BaseResponseModel DeleteReason(DeleteReasonModel model);
        Task<ReasonResponseModel> GetReasons(ReasonRequestModel request);
    }
}
