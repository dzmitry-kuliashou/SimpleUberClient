using Newtonsoft.Json;
using SimpleUber.Errors.ErrorCodes;
using SimpleUberWebApi.Distribution.Client.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SimpleUberWebApi.Distribution.Client.Services.BaseApiClient
{
    public abstract class BaseApiClient<TApiInterface> 
        where TApiInterface : class 
    {
        private readonly WebApiResultHandler _webApiResultHandler;

        protected BaseApiClient()
        {
            _webApiResultHandler = new WebApiResultHandler();
        }

        protected async Task<WebApiResult> SendDeleteRequestAsync(object content)
        {
            throw new NotImplementedException();
        }

        protected async Task<WebApiResult> SendPutRequestAsync(object content)
        {
            throw new NotImplementedException();
        }

        protected async Task<WebApiResult> SendGetRequestAsync()
        {
            using (var client = new HttpClient())
            {
                var routeUri = WebApiCaller.GetRouteUri(typeof(TApiInterface), SimpleUber.Distribution.Api.Common.HttpMethod.Get);
                var requestUri = string.Format("{0}{1}", WebApiCaller.BasicUrl, routeUri);

                if (!string.IsNullOrWhiteSpace(WebApiCaller.Token))
                {
                    client.DefaultRequestHeaders.Add("token", WebApiCaller.Token);
                }

                var responseContent = await client.GetAsync(requestUri);

                return await _webApiResultHandler.HandleResponseContent(responseContent);
            }
        }

        protected async Task<WebApiResult> SendPostRequestAsync(object content)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(WebApiCaller.BasicUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (!string.IsNullOrWhiteSpace(WebApiCaller.Token))
                {
                    client.DefaultRequestHeaders.Add("token", WebApiCaller.Token);
                }

                var routeUri = WebApiCaller.GetRouteUri(typeof(TApiInterface), SimpleUber.Distribution.Api.Common.HttpMethod.Post);
                var responseContent = await client.PostAsJsonAsync(routeUri, content);

                return await _webApiResultHandler.HandleResponseContent(responseContent);
            }
        }

        protected TResult HandleServiceResult<TResult>(WebApiResult webApiResult)
        {
            if(webApiResult.IsSuccessStatusCode)
            {
                try
                {
                    return JsonConvert.DeserializeObject<TResult>(webApiResult.Result);
                }
                catch
                {
                    var errorCode = new ErrorCode(-666, "Service response object deserialization error");
                    throw new ServiceResponseException.ServiceResponseException(new List<ErrorCode> { errorCode });
                }
            }

            throw new ServiceResponseException.ServiceResponseException(webApiResult.ErrorCodes);
        }
    }
}
