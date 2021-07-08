using System.Net;
using JeweleryShopApi.Common;
using JeweleryShopApi.Helpers;
using JeweleryShopApi.Infrastructure;
using JeweleryShopApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace JeweleryShopApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;            
        }

        [HttpGet]
        [PrivilegedUserAuthorize]
        [Route("getdiscount/{productName}")]
        public IActionResult GetDiscount(string productName)
        {
            if(string.IsNullOrEmpty(productName))
            {
                return new ResponseEnvelope
                (
                    HttpStatusCode.BadRequest,
                    Constants.NoDataFoundForGivenProductMessage,
                    null
                );   
            }

            try
            {
                var discount = this.productService.GetDiscount(productName);
                if(discount != -1)
                {
                    return new ResponseEnvelope
                    (
                        HttpStatusCode.OK,
                        Constants.SuccessMessage,
                        new { ApplicableDiscount = discount }
                    );
                }

                return new ResponseEnvelope
                (
                    HttpStatusCode.BadRequest,
                    Constants.NoDataFoundForGivenProductMessage,
                    null
                );
            }
            catch
            {
                throw new ResponseException(
                    HttpStatusCode.InternalServerError,
                    Constants.ErrorOccuredMessage);
            }
        }
    }
}