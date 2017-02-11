using SimpleUber.Distribution.Api.Services.AuthorComments;
using SimpleUber.Distribution.Api.Services.AuthorComments.Entities;
using SimpleUberWebApi.Distribution.Client.Services.BaseApiClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleUberWebApi.Distribution.Client.Services.AuthorComments
{
    public class AuthorCommentsClient : BaseApiClient<IAuthorComments>, IAuthorComments
    {
        public  IEnumerable<AuthorComment> GetAuthorComments()
        {
            var webApiResult = Task.Run(SendGetRequestAsync).Result;
            return HandleServiceResult<IEnumerable<AuthorComment>>(webApiResult);
        }

        public int CreateNewComment(AuthorComment comment)
        {
            var webApiResult = Task.Run(() => SendPostRequestAsync(comment)).Result;
            return HandleServiceResult<int>(webApiResult);
        }
    }
}
