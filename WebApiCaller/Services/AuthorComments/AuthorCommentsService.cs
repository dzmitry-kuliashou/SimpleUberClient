using MVCTypeScriptReact.Model.AuthorComments;
using Newtonsoft.Json;
using SimpleUber.Distribution.Api.Services.AuthorComments;
using SimpleUber.Distribution.Api.Services.AuthorComments.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCaller.Common;

namespace WebApiCaller.Services.AuthorComments
{
    public class AuthorCommentsService
    {
        private readonly IApiClient _apiClient;

        public AuthorCommentsService()
        {
            _apiClient = new ApiClient();
        }

        public async Task<ServiceResponse<List<CommentModel>>> GetAuthorComments()
        {
            var routeUri =
                WebApiCaller.Common.WebApiCaller.GetRouteUri(typeof(IAuthorComments), SimpleUber.Distribution.Api.Common.HttpMethod.Get);

            var webApiResult = await _apiClient.SendGetRequestAsync(routeUri, AuthorizationToken.Token);

            var authorComments = webApiResult.IsSuccessStatusCode
                ? JsonConvert.DeserializeObject<List<AuthorComment>>(webApiResult.Result)
                : new List<AuthorComment>();

            var commentModels = authorComments.Select(x => new CommentModel { Author = x.Author, Text = x.Comment }).ToList();
            return new ServiceResponse<List<CommentModel>>(webApiResult.IsSuccessStatusCode, commentModels, webApiResult.ErrorCodes);
        }

        public async Task<ServiceResponse<int>> PostNewAuthorComment(CommentModel commentModel)
        {
            var authorComment = new AuthorComment { Author = commentModel.Author, Comment = commentModel.Text };

            var routeUri =
                WebApiCaller.Common.WebApiCaller.GetRouteUri(typeof(IAuthorComments), SimpleUber.Distribution.Api.Common.HttpMethod.Post);

            var webApiResult = await _apiClient.SendPostRequestAsync(routeUri, authorComment, AuthorizationToken.Token);
            return new ServiceResponse<int>(webApiResult);
        }
    }
}
