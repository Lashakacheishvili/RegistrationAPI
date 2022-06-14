using Common.Enums;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Helper;
using Service.ServiceInterfaces;
using ServiceModels;
using ServiceModels.User;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Service.ServiceImplementations
{
    public class UserService : IUserService
    {
        private readonly RegistrationContext _dbContext;
        public UserService(RegistrationContext dbContext)
        {
            _dbContext = dbContext;
        }
        public  BaseResponseModel CreateUser(CreateUserModel model)
        {
            model.PasswordHash = new PasswordHasher<Domain.Model.User>().HashPassword(null, model.PasswordHash);
            if (model.UserRole.Equals(UserRole.Child) && string.IsNullOrEmpty(model.ParrentUserName))
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "თქვენ არ გაქვთ დარეგისტრირების უფლება");
            var user = _dbContext.Users.FirstOrDefault(s => s.UserName.Equals(model.UserName));
            if (user != null)
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "ასეთი მომხამრებელი უკვე არსებობს");

            if (!string.IsNullOrEmpty(model.ParrentUserName))
            {
                var parrent = _dbContext.Users.FirstOrDefault(s => s.UserName.Equals(model.ParrentUserName));
                if (parrent != null)
                    return new BaseResponseModel((int)HttpStatusCode.BadRequest, "ასეთი მშობელი არ არსებობს არსებობს");
                _dbContext.Users.Add(new Domain.Model.User
                {
                    UserName = model.UserName,
                    AccessFailedCount = model.AccessFailedCount,
                    UserRole = UserRole.Child,
                    PasswordHash = model.PasswordHash,
                    UserStatus = model.UserStatus,
                    SecurityStamp = model.SecurityStamp,
                    ConcurrencyStamp = model.ConcurrencyStamp,
                    ParrentId=parrent.Id,
                    EmailConfirmed = model.EmailConfirmed,
                    PhoneNumberConfirmed = model.PhoneNumberConfirmed,  
                    TwoFactorEnabled = model.TwoFactorEnabled,
                    LockoutEnabled = model.LockoutEnabled
                });
                parrent.ChildCount += 1;
                _dbContext.Entry(parrent).State = EntityState.Modified;
                if(_dbContext.SaveChanges()>0)
                {
                    return new BaseResponseModel((int)HttpStatusCode.OK, "მონაცემები წარმატებით შეინახა");
                }
            }
            else
            {
                _dbContext.Users.Add(new Domain.Model.User
                {
                    UserName = model.UserName,
                    AccessFailedCount = model.AccessFailedCount,
                    UserRole = UserRole.Parrent,
                    PasswordHash = model.PasswordHash,
                    UserStatus = model.UserStatus,
                    SecurityStamp = model.SecurityStamp,
                    ConcurrencyStamp = model.ConcurrencyStamp,
                    EmailConfirmed = model.EmailConfirmed,
                    PhoneNumberConfirmed = model.PhoneNumberConfirmed,
                    TwoFactorEnabled = model.TwoFactorEnabled,
                    LockoutEnabled = model.LockoutEnabled
                });
                if (_dbContext.SaveChanges() > 0)
                {
                    return new BaseResponseModel((int)HttpStatusCode.OK, "მონაცემები წარმატებით შეინახა");
                }
            }

            return new BaseResponseModel((int)HttpStatusCode.BadRequest, "დაფიქსირდა სისტემური შეცდომა"); 
        }
    }
}
