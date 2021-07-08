using JeweleryShopApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JeweleryShopApi.Infrastructure
{
    public class ResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is ResponseException exception)
            {
                var responseEnvelope = new ResponseEnvelope
                (
                    exception.Code,
                    exception.ErrorInformation,
                    null   
                );
                context.Result = responseEnvelope;
                context.ExceptionHandled = true;
            }
        }
    }
}