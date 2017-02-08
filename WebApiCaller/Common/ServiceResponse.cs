using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApiCaller.Common
{
    public class ServiceResponse<TResult>
    {
        public TResult Result { get; private set; }

        public bool IsSucceed { get; private set; }

        public List<ErrorCode> ErrorCodes { get; private set; }

        public ServiceResponse(WebApiResult webApiResult)
        {
            IsSucceed = webApiResult.IsSuccessStatusCode;
            Result = GetResult(webApiResult);
            ErrorCodes = GetErrorCodes(webApiResult.IsSuccessStatusCode, webApiResult.ErrorCodes);
        }

        public ServiceResponse(bool isSucceed, TResult result, List<SimpleUber.Errors.ErrorCodes.ErrorCode> errorCodes)
        {
            IsSucceed = isSucceed;
            Result = result;
            ErrorCodes = GetErrorCodes(isSucceed, errorCodes);
        }

        private TResult GetResult(WebApiResult webApiResult)
        {
            if (webApiResult.IsSuccessStatusCode)
            {
                try
                {
                    return JsonConvert.DeserializeObject<TResult>(webApiResult.Result);
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }

            return default(TResult);
        }

        private List<ErrorCode> GetErrorCodes(bool isSuccessStatusCode, List<SimpleUber.Errors.ErrorCodes.ErrorCode> errorCodes)
        {
            if(isSuccessStatusCode)
            {
                return new List<ErrorCode>();
            }

            return errorCodes.Select(x => new ErrorCode(x.Code, x.Message)).ToList();
        }
    }
}
