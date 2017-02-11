using MVCTypeScriptReact.Model.AuthorComments;
using System.Collections.Generic;
using WebApiCaller.Common;

namespace WebApiCaller.Services.AuthorComments
{
    public interface IAuthorCommentsService
    {
        ServiceResponse<List<CommentModel>> GetAuthorComments();

        ServiceResponse<int> AddNewAuthorComment(CommentModel commentModel);
    }
}
