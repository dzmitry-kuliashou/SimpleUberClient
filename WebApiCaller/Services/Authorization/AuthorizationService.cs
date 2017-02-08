using SimpleUber.Distribution.Api.Services.Authorization;
using System.Threading.Tasks;
using WebApiCaller.Common;

namespace WebApiCaller.Services.Authorization
{
    public class AuthorizationService
    {
        private readonly IApiClient _apiClient;

        public AuthorizationService()
        {
            _apiClient = new ApiClient();
        }

        public async Task<ServiceResponse<string>> Authorize()
        {
            var routeUri =
                WebApiCaller.Common.WebApiCaller.GetRouteUri(typeof(IAuthorization), SimpleUber.Distribution.Api.Common.HttpMethod.Post);

            var webApiResult = await _apiClient.SendPostRequestAsync(routeUri, null, null);

            if (webApiResult.IsSuccessStatusCode)
            {
                AuthorizationToken.Token = webApiResult.Result.Replace("\"", "");
            }

            return new ServiceResponse<string>(webApiResult.IsSuccessStatusCode, AuthorizationToken.Token, webApiResult.ErrorCodes);
        }
    }
}
