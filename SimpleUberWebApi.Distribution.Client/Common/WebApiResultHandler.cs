using Newtonsoft.Json;
using SimpleUber.Errors.Exception;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SimpleUberWebApi.Distribution.Client.Common
{
    public class WebApiResultHandler
    {
        public async Task<WebApiResult> HandleResponseContent(HttpResponseMessage responseContent)
        {
            if (responseContent.IsSuccessStatusCode)
            {
                var result = await responseContent.Content.ReadAsStringAsync();
                return new WebApiResult(true, result, null);
            }
            else if (responseContent.StatusCode == HttpStatusCode.Unauthorized)
            {
                var errorCode = new SimpleUber.Errors.ErrorCodes.ErrorCode(401, "Authorization required");
                return new WebApiResult(false, null, new List<SimpleUber.Errors.ErrorCodes.ErrorCode> { errorCode });
            }
            else if (responseContent.StatusCode == HttpStatusCode.InternalServerError)
            {
                return await HandleInternalServerError(responseContent);
            }
            else
            {
                var errorCode = new SimpleUber.Errors.ErrorCodes.ErrorCode(500, "Some unhandled error");
                return new WebApiResult(false, null, new List<SimpleUber.Errors.ErrorCodes.ErrorCode> { errorCode });
            }
        }

        private async Task<WebApiResult> HandleInternalServerError(HttpResponseMessage responseContent)
        {
            var resultString = await responseContent.Content.ReadAsStringAsync();

            ValidationErrorForClient validationError;

            if (TryGetValidationError(resultString, out validationError))
            {
                return new WebApiResult(false, null, validationError.ErrorCodes);
            }

            var errorCode = new SimpleUber.Errors.ErrorCodes.ErrorCode(500, resultString);
            return new WebApiResult(false, null, new List<SimpleUber.Errors.ErrorCodes.ErrorCode> { errorCode });
        }

        private bool TryGetValidationError(string resultString, out ValidationErrorForClient validationError)
        {
            validationError = null;

            if (!string.IsNullOrWhiteSpace(resultString))
            {
                try
                {
                    validationError = JsonConvert.DeserializeObject<ValidationErrorForClient>(resultString);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }
    }
}
