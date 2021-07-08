namespace JeweleryShopApi.Tests.Controllers
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using JeweleryShopApi.Services;
    using JeweleryShopApi.Models;
    using JeweleryShopApi.Entities;
    using JeweleryShopApi.Common.Enums;
    using JeweleryShopApi.Controllers;
    using JeweleryShopApi.Helpers;
    using System.Net;
    using JeweleryShopApi.Common;
    using System.Text.Json;

    [TestClass]
    public class UserAuthenticationControllerTests
    {
        Mock<IUserAuthService> authService;

        [TestMethod]
        public void VerifyNoTokenIsReturnedWhenUserNameAndPasswordAreNotPassedInArgument()
        {
            authService = new Mock<IUserAuthService>();
            authService.Setup(s => s.AuthenticateUser(It.IsAny<AuthenticationRequest>())).Returns(string.Empty);
            var userAuthenticationController = new UserAuthenticationController(authService.Object);
            try
            {
                var userAuthResponse = (ResponseEnvelope) userAuthenticationController.AuthenticateUser(new AuthenticationRequest());
            }
            catch (ResponseException responseException)
            {
                Assert.AreEqual(HttpStatusCode.BadRequest, responseException.Code);
                Assert.AreEqual(Constants.MissingParametersMessage, responseException.ErrorInformation);
            }

            authService.Verify(x => x.AuthenticateUser(It.IsAny<AuthenticationRequest>()), Times.Exactly(0));
        }

        [TestMethod]
        public void VerifyNoTokenIsReturnedWhenUserNameIsNotPassedAsArgument()
        {
            authService = new Mock<IUserAuthService>();
            authService.Setup(s => s.AuthenticateUser(It.IsAny<AuthenticationRequest>())).Returns(string.Empty);
            var userAuthenticationController = new UserAuthenticationController(authService.Object);
            try
            {
                var userAuthResponse = (ResponseEnvelope) userAuthenticationController.AuthenticateUser(new AuthenticationRequest{ Password="Password"});
            }
            catch (ResponseException responseException)
            {
                Assert.AreEqual(HttpStatusCode.BadRequest, responseException.Code);
                Assert.AreEqual(Constants.MissingParametersMessage, responseException.ErrorInformation);
            }

            authService.Verify(x => x.AuthenticateUser(It.IsAny<AuthenticationRequest>()), Times.Exactly(0));
        }

        [TestMethod]
        public void VerifyNoTokenIsReturnedWhenPasswordIsNotPassedAsArgument()
        {
            // Arrange
            authService = new Mock<IUserAuthService>();
            authService.Setup(s => s.AuthenticateUser(It.IsAny<AuthenticationRequest>())).Returns(string.Empty);
            var userAuthenticationController = new UserAuthenticationController(authService.Object);
            
            // Act
            try
            {
                var userAuthResponse = (ResponseEnvelope) userAuthenticationController.AuthenticateUser(new AuthenticationRequest{ UserName="Username"});
            }

            // Assert
            catch (ResponseException responseException)
            {
                Assert.AreEqual(HttpStatusCode.BadRequest, responseException.Code);
                Assert.AreEqual(Constants.MissingParametersMessage, responseException.ErrorInformation);
            }

            authService.Verify(x => x.AuthenticateUser(It.IsAny<AuthenticationRequest>()), Times.Exactly(0));
        }

        [TestMethod]
        public void VerifyTokenIsReturnedWhenUserNameAndPasswordPassedAsArgument()
        {
            // Arrange
            authService = new Mock<IUserAuthService>();            
            var testToken = "This is just a test token";
            var expectedResponse = new ResponseEnvelope(HttpStatusCode.OK, Constants.AuthenticationSuccessMessage, new { AuthenticationToken = testToken });
            authService.Setup(s => s.AuthenticateUser(It.IsAny<AuthenticationRequest>())).Returns(testToken);
            var jsonStringExpectedResponseData = JsonSerializer.Serialize(new { AuthenticationToken = testToken });

            // Act
            var userAuthenticationController = new UserAuthenticationController(authService.Object);
            var actualAuthUserResponse = (ResponseEnvelope) userAuthenticationController.AuthenticateUser(new AuthenticationRequest{ UserName="Username", Password="Password"});
            var jsonStringActualResponseData = JsonSerializer.Serialize(actualAuthUserResponse.Data)            ;
            
            // Assert
            Assert.AreEqual(expectedResponse.Code, actualAuthUserResponse.Code);
            Assert.AreEqual(jsonStringExpectedResponseData, jsonStringActualResponseData);
            authService.Verify(x => x.AuthenticateUser(It.IsAny<AuthenticationRequest>()), Times.Exactly(1));
        }
    }
}