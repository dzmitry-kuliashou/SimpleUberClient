using SimpleUber.Distribution.Api.Services.Authorization;
using SimpleUberWebApi.Distribution.Client.Services.BaseApiClient;
using System.Threading.Tasks;

namespace SimpleUberWebApi.Distribution.Client.Services.Authorization
{
    public class AuthorizationClient : BaseApiClient<IAuthorization>, IAuthorization
    {
        public string Authorize()
        {
            var webApiResult = Task.Run(() => SendPostRequestAsync(null)).Result;
            return HandleServiceResult<string>(webApiResult);
        }
    }
}
