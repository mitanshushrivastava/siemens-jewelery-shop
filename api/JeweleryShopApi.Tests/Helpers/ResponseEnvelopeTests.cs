namespace JeweleryShopApi.Tests.Helpers
{
    using System.Net;
    using JeweleryShopApi.Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Text.Json;

    [TestClass]
    public class ResponseEnvelopeTests
    {
        public void VerifyAllPropertiesAreSetCorrectly()
        {
            // Arrange
            var expectedCode = HttpStatusCode.OK;
            var expectedMessage = "Test Message";
            var expectedData = new { Prop1 = "Prop1" };
            var jsonStringExpectedResponseData = JsonSerializer.Serialize(expectedData);

            // Act
            var actualResponseEnvelope = new ResponseEnvelope(expectedCode, expectedMessage, expectedData);
            var jsonStringActualResponseData = JsonSerializer.Serialize(actualResponseEnvelope.Data);
            
            //Assert
            Assert.AreEqual(jsonStringActualResponseData, jsonStringExpectedResponseData);
            Assert.AreEqual(expectedMessage, actualResponseEnvelope.Message);
            Assert.AreEqual(expectedCode, actualResponseEnvelope.Code);
        }
    }
}