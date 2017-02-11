using System.Collections.Generic;
using System.Linq;

namespace WebApiCaller.Common
{
    public class ServiceResponse<TResult>
    {
        public TResult Result { get; private set; }

        public bool IsSucceed { get; private set; }

        public List<ErrorCode> ErrorCodes { get; private set; }

        public ServiceResponse(bool isSucceed, TResult result, List<SimpleUber.Errors.ErrorCodes.ErrorCode> errorCodes)
        {
            IsSucceed = isSucceed;
            Result = result;
            ErrorCodes = GetErrorCodes(isSucceed, errorCodes);
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
