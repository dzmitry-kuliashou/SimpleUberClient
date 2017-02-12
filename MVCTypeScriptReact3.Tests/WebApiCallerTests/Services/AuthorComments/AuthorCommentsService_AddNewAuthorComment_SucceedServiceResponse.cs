using Moq;
using NUnit.Framework;
using SimpleUber.Distribution.Api.Services.AuthorComments;
using System.Collections.Generic;
using WebApiCaller.Common;
using WebApiCaller.Services.AuthorComments;

namespace MVCTypeScriptReact3.Tests.WebApiCallerTests.Services.AuthorComments
{
    [TestFixture]
    public class AuthorCommentsService_AddNewAuthorComment_SucceedServiceResponse
    {
        private ServiceResponse<int> serviceResponse;

        [SetUp]
        public void SetUpTest()
        {
            var mockAuthorCommentsService = new Mock<IAuthorComments>();
            mockAuthorCommentsService
                .Setup(x => x.CreateNewComment(It.IsAny<SimpleUber.Distribution.Api.Services.AuthorComments.Entities.AuthorComment>()))
                .Returns(3);

            var authorCommentService = new AuthorCommentsService(mockAuthorCommentsService.Object);

            serviceResponse = authorCommentService.AddNewAuthorComment(new MVCTypeScriptReact.Model.AuthorComments.CommentModel { Author = "Author1", Text = "Comment1" });
        }

        [Test]
        public void ResponseMustNotBeNull()
        {
            Assert.IsNotNull(serviceResponse);
        }

        [Test]
        public void ResponseMustBeSucceed()
        {
            Assert.AreEqual(true, serviceResponse.IsSucceed);
        }

        [Test]
        public void ErrorCodesCollectionMustBeEmpty()
        {
            Assert.AreEqual(new List<ErrorCode>(), serviceResponse.ErrorCodes);
        }

        [Test]
        public void ResultMustBeCorrect()
        {
            Assert.AreEqual(3, serviceResponse.Result);
        }
    }
}
