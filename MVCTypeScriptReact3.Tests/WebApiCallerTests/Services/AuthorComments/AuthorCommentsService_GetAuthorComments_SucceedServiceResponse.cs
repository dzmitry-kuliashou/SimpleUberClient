using Moq;
using MVCTypeScriptReact.Model.AuthorComments;
using NUnit.Framework;
using SimpleUber.Distribution.Api.Services.AuthorComments;
using SimpleUber.Distribution.Api.Services.AuthorComments.Entities;
using System.Collections.Generic;
using WebApiCaller.Common;
using WebApiCaller.Services.AuthorComments;

namespace MVCTypeScriptReact3.Tests.WebApiCallerTests.Services.AuthorComments
{
    [TestFixture]
    public class AuthorCommentsService_GetAuthorComments_SucceedServiceResponse
    {
        private ServiceResponse<List<CommentModel>> serviceResponse;

        [SetUp]
        public void SetUpTest()
        {
            var mockAuthorCommentsService = new Mock<IAuthorComments>();
            mockAuthorCommentsService.Setup(x => x.GetAuthorComments())
                .Returns(new List<AuthorComment>
                {
                    new AuthorComment {Author = "Author1", Comment = "Comment1" },
                    new AuthorComment {Author = "Author2", Comment = "Comment2" }
                });

            var authorCommentService = new AuthorCommentsService(mockAuthorCommentsService.Object);

            serviceResponse = authorCommentService.GetAuthorComments();
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
        public void MustReturnCollectionOfTwoItems()
        {
            Assert.AreEqual(2, serviceResponse.Result.Count);
        }

        [Test]
        public void ResultMustBeCorrect()
        {
            Assert.Multiple(() =>
            {
                Assert.NotNull(serviceResponse.Result[0]);
                Assert.NotNull(serviceResponse.Result[1]);
                Assert.AreEqual(true, serviceResponse.Result[0].Author == "Author1" && serviceResponse.Result[0].Text == "Comment1");
                Assert.AreEqual(true, serviceResponse.Result[1].Author == "Author2" && serviceResponse.Result[1].Text == "Comment2");
            });
        }
    }
}
