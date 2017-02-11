using MVCTypeScriptReact.Model.AuthorComments;
using SimpleUber.Distribution.Api.Services.AuthorComments;
using SimpleUber.Distribution.Api.Services.AuthorComments.Entities;
using SimpleUber.Errors.ServiceResponseException;
using System.Collections.Generic;
using System.Linq;
using WebApiCaller.Common;

namespace WebApiCaller.Services.AuthorComments
{
    public class AuthorCommentsService : IAuthorCommentsService
    {
        private readonly IAuthorComments _authorCommentsClient;

        public AuthorCommentsService(IAuthorComments authorCommentsClient)
        {
            _authorCommentsClient = authorCommentsClient;
        }

        public ServiceResponse<List<CommentModel>> GetAuthorComments()
        {
            try
            {
                var authorComments = _authorCommentsClient.GetAuthorComments();
                var commentModels = authorComments.Select(x => new CommentModel { Author = x.Author, Text = x.Comment }).ToList();
                return new ServiceResponse<List<CommentModel>>(true, commentModels, null);
            }
            catch(ServiceResponseException ex)
            {
                return new ServiceResponse<List<CommentModel>>(false, null, ex.ErrorCodes);
            }
        }

        public ServiceResponse<int> AddNewAuthorComment(CommentModel commentModel)
        {
            var authorComment = new AuthorComment { Author = commentModel.Author, Comment = commentModel.Text };

            try
            {
                var authorCommentId = _authorCommentsClient.CreateNewComment(authorComment);
                return new ServiceResponse<int>(true, authorCommentId, null);
            }
            catch (ServiceResponseException ex)
            {
                return new ServiceResponse<int>(false, 0, ex.ErrorCodes);
            }
        }
    }
}
