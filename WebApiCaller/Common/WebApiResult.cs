using System.Collections.Generic;

namespace WebApiCaller.Common
{
    public class WebApiResult
    {
        public bool IsSuccessStatusCode { get; private set; }

        public string Result { get; private set; }

        public List<SimpleUber.Errors.ErrorCodes.ErrorCode> ErrorCodes { get; private set; }

        public WebApiResult(bool isSuccessStatusCode, string result, List<SimpleUber.Errors.ErrorCodes.ErrorCode> errorCodes)
        {
            IsSuccessStatusCode = isSuccessStatusCode;
            Result = result;
            ErrorCodes = errorCodes;
        }
    }
}
