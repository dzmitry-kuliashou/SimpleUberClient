﻿using Moq;
using MVCTypeScriptReact.Model.AuthorComments;
using NUnit.Framework;
using SimpleUber.Distribution.Api.Services.AuthorComments;
using SimpleUber.Errors.ServiceResponseException;
using System.Collections.Generic;
using WebApiCaller.Common;
using WebApiCaller.Services.AuthorComments;

namespace MVCTypeScriptReact3.Tests.WebApiCallerTests.Services.AuthorComments
{
    [TestFixture]
    public class AuthorCommentsService_GetAuthroComments_FailedServiceResponse
    {
        private ServiceResponse<List<CommentModel>> serviceResponse;

        [SetUp]
        public void SetUpTest()
        {
            var mockAuthorCommentsService = new Mock<IAuthorComments>();

            var errorCodes = new List<SimpleUber.Errors.ErrorCodes.ErrorCode>
            {
                new SimpleUber.Errors.ErrorCodes.ErrorCode(500, "Some error message1"),
                new SimpleUber.Errors.ErrorCodes.ErrorCode(500, "Some error message2")
            };

            mockAuthorCommentsService.Setup(x => x.GetAuthorComments())
                .Throws(new ServiceResponseException(errorCodes));

            var authorCommentService = new AuthorCommentsService(mockAuthorCommentsService.Object);

            serviceResponse = authorCommentService.GetAuthorComments();
        }

        [Test]
        public void ResponseMustNotBeNull()
        {
            Assert.IsNotNull(serviceResponse);
        }

        [Test]
        public void ResponseMustNotBeSucceed()
        {
            Assert.AreEqual(false, serviceResponse.IsSucceed);
        }

        [Test]
        public void ResultItemMustBeEmptyCollection()
        {
            Assert.AreEqual(new List<CommentModel>(), serviceResponse.Result);
        }

        [Test]
        public void ErrorCodesCollectionMustNotBeEmpty()
        {
            Assert.AreEqual(2, serviceResponse.ErrorCodes.Count);
        }

        [Test]
        public void ResponseCodesMustBeCorrect()
        {
            Assert.Multiple(() =>
            {
                Assert.NotNull(serviceResponse.ErrorCodes[0]);
                Assert.NotNull(serviceResponse.ErrorCodes[1]);
                Assert.AreEqual(true, serviceResponse.ErrorCodes[0].Code == 500 && serviceResponse.ErrorCodes[0].Message == "Some error message1");
                Assert.AreEqual(true, serviceResponse.ErrorCodes[1].Code == 500 && serviceResponse.ErrorCodes[1].Message == "Some error message2");
            });
        }
    }
}
