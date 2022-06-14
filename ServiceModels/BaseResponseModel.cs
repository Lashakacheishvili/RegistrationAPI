using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceModels
{
    public class BaseResponseModel
    {
        public int HttpStatusCode { get; protected set; }
        public string Message { get; protected set; }
        public BaseResponseModel(int httpStatusCode, string message)
        {
            HttpStatusCode = httpStatusCode;
            Message = message;
        }
    }
    public class CreateBaseResponseModel : BaseResponseModel
    {
        public int Id { get; set; }
        public CreateBaseResponseModel(int id, int httpStatusCode, string message) : base(httpStatusCode, message)
        {
            Id = id;
        }
    }
}
