using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HepsiYemek.API.Models
{
    public class ServiceResponse
    {
        public bool isSuccess { get; set; }
        public dynamic Data { get; set; }
        public ErrorDetail Error { get; set; }

        public ServiceResponse()
        {

        }
        public ServiceResponse(dynamic data)
        {
            this.isSuccess = true;
            this.Data = data;
        }
        public ServiceResponse(ErrorDetail errorDetail)
        {
            this.isSuccess = false;
            this.Error = errorDetail;
        }

    }

    public class ErrorDetail
    {
        public string StackTrace { get; set; }
        public string InnerException { get; set; }
        public string Message { get; set; }
        public string ClientMessage { get; set; }
        public int ErrorCode { get; set; }
    }
}
