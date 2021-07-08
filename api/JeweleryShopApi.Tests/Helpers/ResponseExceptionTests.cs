using System.Net;
using JeweleryShopApi.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JeweleryShopApi.Tests.Helpers
{
    [TestClass]
    public class ResponseExceptionTests
    {
        [TestMethod]
        public void VerifyCorrectValuesAreSet() 
        {
            // Arrange
            var expectedCode = HttpStatusCode.OK;
            var expectedErrorInformation = "This is a test message";
            
            // Act
            var actualResponseException = new ResponseException(expectedCode, expectedErrorInformation);

            // Assert
            Assert.AreEqual(expectedCode, actualResponseException.Code);
            Assert.AreEqual(expectedErrorInformation, actualResponseException.ErrorInformation);
        }
    }
}