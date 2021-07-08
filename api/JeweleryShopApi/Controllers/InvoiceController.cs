namespace JeweleryShopApi.Controllers
{
    using JeweleryShopApi.Common;
    using JeweleryShopApi.Common.Enums;
    using JeweleryShopApi.Entities;
    using JeweleryShopApi.Helpers;
    using JeweleryShopApi.Infrastructure;
    using JeweleryShopApi.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller to get invoices.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [CommonAuthorize]
    public class InvoiceController : ControllerBase
    {
        [HttpGet("getpdfinvoice")]
        public IActionResult GetPdfInvoice([FromQuery] InvoiceRequest invoiceRequest)
        {
            try
            {
                var userAccount = (User)Request.HttpContext.Items[Constants.UserAccountKey];
                var isDiscountApplicable = userAccount.Role == UserRole.PrivilegedUser ? true : false;
                var memoryStream = invoiceRequest.GetPdfInvoice(isDiscountApplicable);
                FileStreamResult fileStreamResult = new FileStreamResult(memoryStream, "application/pdf");
                fileStreamResult.FileDownloadName = "JeweleryShop.pdf";
                return fileStreamResult;
            }
            catch
            {
                throw new ResponseException(
                    System.Net.HttpStatusCode.InternalServerError,
                    "An error occured while generating the PDF file"
                );
            }
        }
    }
}