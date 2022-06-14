using ServiceModels;
using ServiceModels.User;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces
{
    public interface IUserService
    {
        BaseResponseModel CreateUser(CreateUserModel model);
    }
}
