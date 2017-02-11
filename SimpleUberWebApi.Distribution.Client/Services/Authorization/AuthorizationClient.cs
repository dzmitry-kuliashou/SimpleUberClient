using SimpleUber.Distribution.Api.Services.Authorization;
using SimpleUberWebApi.Distribution.Client.Services.BaseApiClient;

namespace SimpleUberWebApi.Distribution.Client.Services.Authorization
{
    public class AuthorizationClient : BaseApiClient<IAuthorization>, IAuthorization
    {
        public string Authorize()
        {
            return SendPost<string>(null);
        }
    }
}
