using Moq;
using NUnit.Framework;
using SimpleUber.Distribution.Api.Services.Authorization;
using SimpleUber.Errors.ServiceResponseException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCaller.Common;
using WebApiCaller.Services.Authorization;

namespace MVCTypeScriptReact3.Tests.WebApiCallerTests.Services.Authorization
{
    [TestFixture]
    public class AuthorizationService_Authorize_FailedServiceResponse
    {
        private ServiceResponse<string> serviceResponse;

        [SetUp]
        public void SetUpTest()
        {
            var mockAuthorizationService = new Mock<IAuthorization>();

            var errorCodes = new List<SimpleUber.Errors.ErrorCodes.ErrorCode>
            {
                new SimpleUber.Errors.ErrorCodes.ErrorCode(500, "Some error message1"),
                new SimpleUber.Errors.ErrorCodes.ErrorCode(500, "Some error message2")
            };

            mockAuthorizationService
                .Setup(x => x.Authorize())
                .Throws(new ServiceResponseException(errorCodes));

            var authorizationService = new AuthorizationService(mockAuthorizationService.Object);

            SimpleUber.Client.Common.WebApiCaller.Token = "12345";

            serviceResponse = authorizationService.Authorize();
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
        public void ResultItemMustBeEmptyString()
        {
            Assert.AreEqual(string.Empty, serviceResponse.Result);
        }

        [Test]
        public void ErrorCodesCollectionMustNotBeEmpty()
        {
            Assert.AreEqual(2, serviceResponse.ErrorCodes.Count);
        }

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

        [Test]
        public void EmptyAuthorizationTokenMustBeSet()
        {
            Assert.AreEqual(string.Empty, SimpleUber.Client.Common.WebApiCaller.Token);
        }
    }
}
