using SimpleUber.Distribution.Api.Services.Authorization;
using SimpleUberWebApi.Distribution.Client.ServiceResponseException;
using WebApiCaller.Common;

namespace WebApiCaller.Services.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IAuthorization _authorizationClient;

        public AuthorizationService(IAuthorization authorizationClient)
        {
            _authorizationClient = authorizationClient;
        }

        public ServiceResponse<string> Authorize()
        {
            try
            {
                var token = _authorizationClient.Authorize().Replace("\"", "");
                SimpleUberWebApi.Distribution.Client.Common.WebApiCaller.Token = token;

                return new ServiceResponse<string>(true, token, null);
            }
            catch (ServiceResponseException ex)
            {
                return new ServiceResponse<string>(false, string.Empty, ex.ErrorCodes);
            }
        }
    }
}
