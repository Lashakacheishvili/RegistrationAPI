using Domain;
using Microsoft.EntityFrameworkCore;
using Service.Helper;
using Service.ServiceInterfaces;
using ServiceModels;
using ServiceModels.Payment;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Service.ServiceImplementations
{
    public class PaymentService : IPaymentService
    {
        private readonly RegistrationContext _dbContext;
        public PaymentService(RegistrationContext dbContext)
        {
            _dbContext = dbContext;
        }
        public BaseResponseModel CreatePayment(CreatePaymentModel model, int? userId)
        {
            if (userId.GetValueOrDefault() <= 0)
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "ავტორიზაცია გაიარე");
            var user = _dbContext.Users.FirstOrDefault(s => s.Id == userId);
            if (user == null)
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "მშობელი ვერ მოიძებნა");
            if (user.ParrentId.HasValue)
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "ჩარიცხვა მხოლოდ მშობელს შეუძლია");
            if (user.Balance - model.Amount < 0)
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "მშობელს არ აქვს საკმარისი თანხა");
            var child = _dbContext.Users.FirstOrDefault(s => s.Id == model.ChildId);
            if (child == null)
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "შვილი ვერ მოიძებნა");
            var reason = _dbContext.Reasons.FirstOrDefault(s => s.Id == model.ReasonId && s.ParrentId == userId);
            if (reason == null)
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "მიზანი ვერ მოიძებნა");
            _dbContext.Payments.Add(new Domain.Model.Payment
            {
                Amount = model.Amount,
                ChildId = model.ChildId,
                ParrentId = user.Id,
                ReasonId = reason.Id
            });
            if (_dbContext.SaveChanges() > 0)
            {
                user.Balance -= model.Amount;
                _dbContext.Entry(user).State = EntityState.Modified;
                child.Balance += model.Amount;
                _dbContext.Entry(child).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            return new BaseResponseModel((int)HttpStatusCode.OK, "მონაცემები წარმატებით შეინახა");
        }
        public async Task<PaymentResponseModel> GetPayments(PaymentRequestModel request, int? userId)
        {
            if (userId.GetValueOrDefault() <= 0)
                return new PaymentResponseModel();
            request.Id = request.Id <= 0 ? userId.Value : request.Id;
            request.ChildId = new System.Collections.Generic.List<int>();
            var mainUset = _dbContext.Users.FirstOrDefault(s => s.Id == request.Id);
            if (mainUset == null)
                return new PaymentResponseModel();
            if (mainUset.Id != userId && mainUset.ParrentId != userId)
                return new PaymentResponseModel();
            request.ChildId.Add(mainUset.Id);
            if (!mainUset.ParrentId.HasValue)
            {
                var childs = _dbContext.Users.Where(s => s.ParrentId == request.Id).Select(s => s.Id).ToList();
                if (childs.Count > 0)
                    request.ChildId.AddRange(childs);
            }
            if (request.ChildId.Count == 0)
                return new PaymentResponseModel();
            var gen = new CRUDGenerator<PaymentRequestModel, Domain.Model.Payment, PaymentItemModel>(request, _dbContext.Database.GetDbConnection());
            var resp = await gen.GenerateSelectAndCount(generationWhere: true);
            return new PaymentResponseModel
            {
                Payments = resp.List,
                TotalCount = resp.TotalCount
            };
        }
        public BaseResponseModel CheckoutReason(int reasonId, int? userId)
        {
            if (userId.GetValueOrDefault() <= 0)
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "ავტორიზაცია გაიარე");
            var user = _dbContext.Users.FirstOrDefault(s => s.Id == userId);
            if (user == null)
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "იუზერი ვერ მოიძებნა");
            var reason = _dbContext.Reasons.FirstOrDefault(s => s.Id == reasonId && s.ParrentId == userId);
            if (reason == null)
            {
                reason = _dbContext.Reasons.FirstOrDefault(s => s.Id == reasonId && s.ChildId == userId);
                if (reason == null)
                    return new BaseResponseModel((int)HttpStatusCode.BadRequest, "მიზანი ვერ მოიძებნა");
            }
            if (reason.IsSuccessed)
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "მიზანი უკვე შესრულებულია");
            if (reason.Amount > user.Balance)
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "მიზნისთვის არ გაქვთ საჭირო თანხა");
            _dbContext.Payments.Add(new Domain.Model.Payment
            {
                Amount = reason.Amount,
                ChildId = !user.ParrentId.HasValue ? (int?)null : reason.ChildId,
                ParrentId = user.ParrentId.HasValue ? (int?)null : user.Id,
                ReasonId = reason.Id
            });
            if (_dbContext.SaveChanges() > 0)
            {
                user.Balance -= reason.Amount;
                _dbContext.Entry(user).State = EntityState.Modified;
                reason.IsSuccessed = true;
                _dbContext.Entry(reason).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            return new BaseResponseModel((int)HttpStatusCode.OK, "მონაცემები წარმატებით შეინახა");

        }
    }
}
