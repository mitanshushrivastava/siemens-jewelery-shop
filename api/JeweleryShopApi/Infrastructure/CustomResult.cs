using System.Threading.Tasks;
using JeweleryShopApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace JeweleryShopApi.Infrastructure
{
    public class CustomResult : IActionResult
    {
        private readonly ResponseEnvelope responseEnvelope;

        public CustomResult(ResponseEnvelope responseEnvelope)
        {
            this.responseEnvelope = responseEnvelope;
        }
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(responseEnvelope)
            {
                StatusCode = (int)responseEnvelope.Code
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}