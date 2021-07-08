using System;
using System.Net;
using JeweleryShopApi.Common;
using JeweleryShopApi.Common.Enums;
using JeweleryShopApi.Entities;
using JeweleryShopApi.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JeweleryShopApi.Infrastructure
{
    public class CommonAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userAccount = (User)context.HttpContext.Items[Constants.UserAccountKey];
            if (userAccount == null || (userAccount.Role != UserRole.NormalUser && userAccount.Role != UserRole.PrivilegedUser))
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.HttpContext.Response.WriteAsJsonAsync(new ResponseEnvelope(HttpStatusCode.Unauthorized, Constants.UnauthorizedAccessMessage, null));
            }
        }
    }
}