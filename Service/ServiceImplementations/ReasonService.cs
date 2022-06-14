using Domain;
using Microsoft.EntityFrameworkCore;
using Service.Helper;
using Service.ServiceInterfaces;
using ServiceModels;
using ServiceModels.Reason;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Service.ServiceImplementations
{
    public class ReasonService : IReasonService
    {
        private readonly RegistrationContext _dbContext;
        public ReasonService(RegistrationContext dbContext)
        {
            _dbContext = dbContext;
        }
        public BaseResponseModel CreateReason(CreateEditReasonModel model, int? userId)
        {
            if (model == null)
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "Model არ შეიძლება ცარიელი იყოს");
            if (!userId.HasValue && !model.ChildId.HasValue)
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "Parrent  და Child არ შეიძლება ცარიელი იყოს");
            if (userId.HasValue && !model.ChildId.HasValue)
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "Child არ შეიძლება ცარიელი იყოს");
            if (string.IsNullOrEmpty(model.Name))
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "მიზეზის მითითება აუციებელია");
            if (model.Amount > 0)
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "თანხის მითითება აუციებელია");
            if (!_dbContext.Users.Any(s => s.Id == userId.Value))
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "Parrent არ არსებობს");
            if (!_dbContext.Users.Any(s => s.Id == model.ChildId.Value))
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "Child არ არსებობს");
            if (!_dbContext.Users.Any(s => s.Id == model.ChildId.Value && s.ParrentId == userId.Value))
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "ეს შვილი ამ მშობელს არ ეკუთვნის");
            _dbContext.Reasons.Add(new Domain.Model.Reason
            {
                Amount = model.Amount,
                Name = model.Name,
                ChildId = model.ChildId,
                Description = model.Description,
                ParrentId = userId,
                Required = model.Required,
            });
            if (_dbContext.SaveChanges() > 0)
            {
                return new BaseResponseModel((int)HttpStatusCode.OK, "მონაცემები წარმატებით შეინახა");
            }
            return new BaseResponseModel((int)HttpStatusCode.BadRequest, "დაფიქსირდა სისტემური შეცდომა");
        }
        public BaseResponseModel EditReason(CreateEditReasonModel model, int? userId)
        {
            if (model == null)
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "Model არ შეიძლება ცარიელი იყოს");
            if (!model.Id.HasValue)
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "Id არ შეიძლება ცარიელი იყოს");
            if (!userId.HasValue && !model.ChildId.HasValue)
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "Parrent  და Child არ შეიძლება ცარიელი იყოს");
            if (userId.HasValue && !model.ChildId.HasValue)
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "Child არ შეიძლება ცარიელი იყოს");
            if (string.IsNullOrEmpty(model.Name))
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "მიზეზის მითითება აუციებელია");
            if (model.Amount > 0)
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "თანხის მითითება აუციებელია");
            if (!_dbContext.Users.Any(s => s.Id == userId.Value))
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "Parrent არ არსებობს");
            if (!_dbContext.Users.Any(s => s.Id == model.ChildId.Value))
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "Child არ არსებობს");
            if (!_dbContext.Users.Any(s => s.Id == model.ChildId.Value && s.ParrentId == userId.Value))
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "ეს შვილი ამ მშობელს არ ეკუთვნის");
            var reason = _dbContext.Reasons.FirstOrDefault(s => s.Id == model.Id.Value);
            if (reason == null)
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "ასეთი მიზეზი არ არსებობს");
            reason.Amount = model.Amount;
            reason.Name = model.Name;
            reason.ChildId = model.ChildId;
            reason.Description = model.Description;
            reason.Required = model.Required;
            _dbContext.Entry(reason).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return new BaseResponseModel((int)HttpStatusCode.OK, "მონაცემები წარმატებით შეინახა");
        }
        public BaseResponseModel DeleteReason(DeleteReasonModel model, int? userId)
        {
            var reason = _dbContext.Reasons.FirstOrDefault(s => s.Id == model.Id && s.ParrentId == userId);
            if (reason == null)
                return new BaseResponseModel((int)HttpStatusCode.BadRequest, "ასეთი მიზეზი არ არსებობს");
            reason.DeleteDate = DateTime.UtcNow.AddHours(4);
            _dbContext.Entry(reason).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return new BaseResponseModel((int)HttpStatusCode.OK, "მონაცემები წარმატებით წაიშალა");
        }
        public async Task<ReasonResponseModel> GetReasons(ReasonRequestModel request, int? userId)
        {
            if (userId.GetValueOrDefault() <= 0)
                return new ReasonResponseModel();
            request.Id = request.Id <= 0 ? userId.Value : request.Id;
            request.ChildId = new System.Collections.Generic.List<int>();
            var mainUset = _dbContext.Users.FirstOrDefault(s => s.Id == request.Id);
            if (mainUset == null)
                return new ReasonResponseModel();
            if(mainUset.Id!=userId && mainUset.ParrentId!=userId)
                return new ReasonResponseModel();
            request.ChildId.Add(mainUset.Id);
            if (!mainUset.ParrentId.HasValue)
            {
                var childs = _dbContext.Users.Where(s => s.ParrentId == request.Id).Select(s => s.Id).ToList();
                if (childs.Count > 0)
                    request.ChildId.AddRange(childs);
            }
            if (request.ChildId.Count == 0)
                return new ReasonResponseModel();
            var gen = new CRUDGenerator<ReasonRequestModel, Domain.Model.Reason, ReasonItemModel>(request, _dbContext.Database.GetDbConnection());
            var resp = await gen.GenerateSelectAndCount(generationWhere: true);
            return new ReasonResponseModel
            {
                Reasons = resp.List,
                TotalCount = resp.TotalCount
            };
        }

    }
}
