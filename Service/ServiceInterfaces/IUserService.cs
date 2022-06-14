using ServiceModels;
using ServiceModels.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces
{
    public interface IUserService
    {
        BaseResponseModel CreateUser(CreateUserModel model);
        BaseResponseModel UpdateUser(UpdateUserModel model, int? userId);
        BaseResponseModel ChangePassword(string password, int? userId);
        UserProfileModel GetUser(int? userId);
        List<UserProfileModel> GetChilds(int? userId);
    }
}
