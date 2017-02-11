using SimpleUber.Distribution.Api.Common;
using System;
using System.Reflection;

namespace SimpleUberWebApi.Distribution.Client.Common
{
    public static class WebApiCaller
    {
        public static readonly string BasicUrl = "http://localhost:100/";

        public static string Token { get; set; }

        public static string GetRouteUri(Type type, HttpMethod httpMethod)
        {
            var routeUri = string.Empty;

            var methods = type.GetMethods();

            if (methods != null)
            {
                foreach (var method in methods)
                {
                    RouteUriAttribute attr = method.GetCustomAttribute<RouteUriAttribute>();

                    if (attr != null && attr.HttpMethod == httpMethod)
                    {
                        routeUri = attr.Uri;
                        break;
                    }
                }
            }

            return routeUri;
        }
    }
}
