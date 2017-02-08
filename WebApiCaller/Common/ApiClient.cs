using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebApiCaller.Common
{
    public class ApiClient : IApiClient
    {
        private readonly WebApiResultHandler _webApiResultHandler;

        public ApiClient()
        {
            _webApiResultHandler = new WebApiResultHandler();
        }

        public async Task<WebApiResult> SendDeleteRequestAsync(string routeUri, object content, string authorizationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<WebApiResult> SendPutRequestAsync(string routeUri, object content, string authorizationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<WebApiResult> SendGetRequestAsync(string routeUri, string authorizationToken)
        {
            using (var client = new HttpClient())
            {
                var requestUri = string.Format("{0}{1}", WebApiCaller.BasicUrl, routeUri);

                if (!string.IsNullOrWhiteSpace(authorizationToken))
                {
                    client.DefaultRequestHeaders.Add("token", authorizationToken);
                }

                var responseContent = await client.GetAsync(requestUri);

                return await _webApiResultHandler.HandleResponseContent(responseContent);
            }
        }

        public async Task<WebApiResult> SendPostRequestAsync(string routeUri, object content, string authorizationToken)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(WebApiCaller.BasicUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if(!string.IsNullOrWhiteSpace(authorizationToken))
                {
                    client.DefaultRequestHeaders.Add("token", authorizationToken);
                }

                var responseContent = await client.PostAsJsonAsync(routeUri, content);

                return await _webApiResultHandler.HandleResponseContent(responseContent);
            }
        }
    }
}
