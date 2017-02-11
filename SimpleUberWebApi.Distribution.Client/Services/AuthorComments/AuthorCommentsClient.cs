using SimpleUber.Distribution.Api.Services.AuthorComments;
using SimpleUber.Distribution.Api.Services.AuthorComments.Entities;
using SimpleUberWebApi.Distribution.Client.Services.BaseApiClient;
using System.Collections.Generic;

namespace SimpleUberWebApi.Distribution.Client.Services.AuthorComments
{
    public class AuthorCommentsClient : BaseApiClient<IAuthorComments>, IAuthorComments
    {
        public  IEnumerable<AuthorComment> GetAuthorComments()
        {
            return SendGet<IEnumerable<AuthorComment>>();
        }

        public int CreateNewComment(AuthorComment comment)
        {
            return SendPost<int>(comment);
        }
    }
}
