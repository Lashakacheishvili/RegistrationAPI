using Common.Enums;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Helper;
using Service.ServiceInterfaces;
using ServiceModels;
using ServiceModels.User;
using System;
using System.Collections.Generic;
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
        public BaseResponseModel CreateUser(CreateUserModel model)
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
                    ParrentId = parrent.Id,
                    EmailConfirmed = model.EmailConfirmed,
                    PhoneNumberConfirmed = model.PhoneNumberConfirmed,
                    TwoFactorEnabled = model.TwoFactorEnabled,
                    LockoutEnabled = model.LockoutEnabled
                });
                parrent.ChildCount += 1;
                _dbContext.Entry(parrent).State = EntityState.Modified;
                if (_dbContext.SaveChanges() > 0)
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
                    LockoutEnabled = model.LockoutEnabled,
                    Balance=10000
                });
                if (_dbContext.SaveChanges() > 0)
                {
                    return new BaseResponseModel((int)HttpStatusCode.OK, "მონაცემები წარმატებით შეინახა");
                }
            }

            return new BaseResponseModel((int)HttpStatusCode.BadRequest, "დაფიქსირდა სისტემური შეცდომა");
        }
        public BaseResponseModel UpdateUser(UpdateUserModel model, int? userId)
        {
            if (userId.GetValueOrDefault() <= 0)
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "ავტორიზაცია გაიარე");
            var user = _dbContext.Users.FirstOrDefault(s => s.Id == userId);
            if (user == null)
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "იუზერი ვერ მოიძებნა");
            user.Name = model.Name;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.ProfileImage = model.Image;
            _dbContext.Entry(user).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return new BaseResponseModel((int)HttpStatusCode.OK, "მონაცემები წარმატებით განახლდა");
        }
        public BaseResponseModel ChangePassword(string password, int? userId)
        {
            if (userId.GetValueOrDefault() <= 0)
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "ავტორიზაცია გაიარე");
            var user = _dbContext.Users.FirstOrDefault(s => s.Id == userId);
            if (user == null)
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "იუზერი ვერ მოიძებნა");
            password = new PasswordHasher<Domain.Model.User>().HashPassword(null, password);
            user.PasswordHash = password;
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.ConcurrencyStamp = Guid.NewGuid().ToString();
            _dbContext.SaveChanges();
            return new BaseResponseModel((int)HttpStatusCode.OK, "მონაცემები წარმატებით განახლდა");
        }
        public UserProfileModel GetUser(int? userId)
        {
            if (userId.GetValueOrDefault() <= 0)
                return new UserProfileModel();
            var user = _dbContext.Users.FirstOrDefault(s => s.Id == userId);
            if (user == null)
                return new UserProfileModel();
            return new UserProfileModel
            {
                Balance = user.Balance,
                Email = user.Email,
                Id = user.Id,
                Image = user.ProfileImage,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName
            };
        }
        public List<UserProfileModel> GetChilds(int? userId)
        {
            if (userId.GetValueOrDefault() <= 0)
                return new List<UserProfileModel>();
            var user = _dbContext.Users.FirstOrDefault(s => s.Id == userId);
            if (user == null)
                return new List<UserProfileModel>();
            var response=new List<UserProfileModel>();
            response= _dbContext.Users.Where(s => s.ParrentId == userId).Select(s => new UserProfileModel
            {
                Balance = s.Balance,
                Email = s.Email,
                Id = s.Id,
                Image = s.ProfileImage,
                Name = s.Name,
                PhoneNumber = s.PhoneNumber,
                UserName = s.UserName,
            }).ToList();
            return response;
        }
    }
}
