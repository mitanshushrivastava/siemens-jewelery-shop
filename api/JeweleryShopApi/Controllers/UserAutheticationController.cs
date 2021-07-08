namespace JeweleryShopApi.Controllers
{
    using System.Net;
    using JeweleryShopApi.Common;
    using JeweleryShopApi.Helpers;
    using JeweleryShopApi.Models;
    using JeweleryShopApi.Services;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    
    [DisableCors]
    [ApiController]
    [Route("[controller]")]
    public class UserAuthenticationController : ControllerBase
    {
        IUserAuthService userAuthService;

        public UserAuthenticationController(IUserAuthService userAuthService)
        {
            this.userAuthService = userAuthService;
        }

        [HttpPost("authenticate")]
        public IActionResult AuthenticateUser([FromBody] AuthenticationRequest request)
        {
            if(request == null || string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            {
                throw new ResponseException(
                    HttpStatusCode.BadRequest,
                    Constants.MissingParametersMessage);
            }
            
            try
            {
                var authToken = this.userAuthService.AuthenticateUser(request);
                if(authToken != null)
                {
                    return new ResponseEnvelope
                    (
                        HttpStatusCode.OK,
                        Constants.AuthenticationSuccessMessage,
                        new { AuthenticationToken = authToken }
                    );
                }
                
                return new ResponseEnvelope
                (
                    HttpStatusCode.Unauthorized,
                    Constants.WrongCredentialsErrorContent,
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
