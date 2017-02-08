using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCaller.Common
{
    public interface IApiClient
    {
        Task<WebApiResult> SendPostRequestAsync(string routeUri, object content, string authorizationToken);

        Task<WebApiResult> SendPutRequestAsync(string routeUri, object content, string authorizationToken);

        Task<WebApiResult> SendGetRequestAsync(string routeUri, string authorizationToken);

        Task<WebApiResult> SendDeleteRequestAsync(string routeUri, object content, string authorizationToken);
    }
}
