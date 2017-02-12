using Moq;
using NUnit.Framework;
using SimpleUber.Distribution.Api.Services.Authorization;
using System.Collections.Generic;
using WebApiCaller.Common;
using WebApiCaller.Services.Authorization;

namespace MVCTypeScriptReact3.Tests.WebApiCallerTests.Services.Authorization
{
    [TestFixture]
    public class AuthorizationService_Authorize_SucceedServiceResponse
    {
        private ServiceResponse<string> serviceResponse;

        [SetUp]
        public void SetUpTest()
        {
            var mockAuthorizationService = new Mock<IAuthorization>();
            mockAuthorizationService
                .Setup(x => x.Authorize())
                .Returns("12345");

            var authorizationService = new AuthorizationService(mockAuthorizationService.Object);

            serviceResponse = authorizationService.Authorize();
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
            Assert.AreEqual("12345", serviceResponse.Result);
        }

        [Test]
        public void AuthorizationTokenMustBeSet()
        {
            Assert.AreEqual("12345", SimpleUber.Client.Common.WebApiCaller.Token);
        }
    }
}
