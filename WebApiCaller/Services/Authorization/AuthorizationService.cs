using SimpleUber.Distribution.Api.Services.Authorization;
using SimpleUber.Errors.ServiceResponseException;
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

                //TODO change it some way to avoid uding SimpleUber.Client libraty
                SimpleUber.Client.Common.WebApiCaller.Token = token;

                return new ServiceResponse<string>(true, token, null);
            }
            catch (ServiceResponseException ex)
            {
                SimpleUber.Client.Common.WebApiCaller.Token = string.Empty;

                return new ServiceResponse<string>(false, string.Empty, ex.ErrorCodes);
            }
        }
    }
}
