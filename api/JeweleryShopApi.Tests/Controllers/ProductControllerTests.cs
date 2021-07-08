using System.Net;
using JeweleryShopApi.Common;
using JeweleryShopApi.Controllers;
using JeweleryShopApi.Helpers;
using JeweleryShopApi.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Text.Json;

namespace JeweleryShopApi.Tests.Controllers
{
    [TestClass]
    public class ProductControllerTests
    {
        Mock<IProductService> productService;

        [TestMethod]
        public void VerifyCorrectDiscountIsReturnedInResponseForCorrectProductName()
        {
            // Arrange
            productService = new Mock<IProductService>();
            var discount = 3;
            productService.Setup(x => x.GetDiscount(It.IsAny<string>())).Returns(discount);
            var productController = new ProductController(productService.Object);
            var productName = "gold";
            var expectedResponse = new ResponseEnvelope(HttpStatusCode.OK, Constants.SuccessMessage, new { ApplicableDiscount = discount });
            var jsonStringExpectedResponseData = JsonSerializer.Serialize(expectedResponse.Data);

            // Act
            var actualResponse = (ResponseEnvelope)productController.GetDiscount(productName);
            var jsonStringActualResponseData = JsonSerializer.Serialize(actualResponse.Data);

            // Assert
            Assert.AreEqual(expectedResponse.Code, actualResponse.Code);
            Assert.AreEqual(expectedResponse.Message, actualResponse.Message);
            productService.Verify(x => x.GetDiscount(It.IsAny<string>()), Times.Exactly(1));
            Assert.AreEqual(jsonStringExpectedResponseData, jsonStringActualResponseData);
        }

        [TestMethod]
        public void VerifyErrorResponseWhenProductNameIsNull()
        {
            // Arrange
            productService = new Mock<IProductService>();
            var discount = 3;
            productService.Setup(x => x.GetDiscount(It.IsAny<string>())).Returns(discount);
            var productController = new ProductController(productService.Object);
            string productName = null;
            var expectedResponse = new ResponseEnvelope(HttpStatusCode.BadRequest, Constants.NoDataFoundForGivenProductMessage, null);
            var jsonStringExpectedResponseData = JsonSerializer.Serialize(expectedResponse.Data);

            // Act
            var actualResponse = (ResponseEnvelope)productController.GetDiscount(productName);
            var jsonStringActualResponseData = JsonSerializer.Serialize(actualResponse.Data);

            // Assert
            Assert.AreEqual(expectedResponse.Code, actualResponse.Code);
            Assert.AreEqual(expectedResponse.Message, actualResponse.Message);
            productService.Verify(x => x.GetDiscount(It.IsAny<string>()), Times.Exactly(0));
            Assert.AreEqual(jsonStringExpectedResponseData, jsonStringActualResponseData);
        }

        [TestMethod]
        public void VerifyErrorResponseWhenDiscountIsMinus1()
        {
            // Arrange
            productService = new Mock<IProductService>();
            var discount = -1;
            productService.Setup(x => x.GetDiscount(It.IsAny<string>())).Returns(discount);
            var productController = new ProductController(productService.Object);
            string productName = "gold";
            var expectedResponse = new ResponseEnvelope(HttpStatusCode.BadRequest, Constants.NoDataFoundForGivenProductMessage, null);
            var jsonStringExpectedResponseData = JsonSerializer.Serialize(expectedResponse.Data);

            // Act
            var actualResponse = (ResponseEnvelope)productController.GetDiscount(productName);
            var jsonStringActualResponseData = JsonSerializer.Serialize(actualResponse.Data);

            // Assert
            Assert.AreEqual(expectedResponse.Code, actualResponse.Code);
            Assert.AreEqual(expectedResponse.Message, actualResponse.Message);
            productService.Verify(x => x.GetDiscount(It.IsAny<string>()), Times.Exactly(1));
            Assert.AreEqual(jsonStringExpectedResponseData, jsonStringActualResponseData);
        }
    }
}