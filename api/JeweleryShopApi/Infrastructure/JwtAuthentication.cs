namespace JeweleryShopApi.Helpers
{
    using JeweleryShopApi.Common;
    using JeweleryShopApi.Common.Enums;
    using JeweleryShopApi.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public class JwtAuthentication
    {
        private readonly RequestDelegate nextRequest;
        private readonly ApplicationSettings settings;

        public JwtAuthentication(RequestDelegate nextRequest, IOptions<ApplicationSettings> settings)
        {
            this.nextRequest = nextRequest;
            this.settings = settings.Value;
        }

        public async Task Invoke(HttpContext context, IUserAuthService authService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                this.AttachAccountToContext(context, token, authService);
            }

            await this.nextRequest(context);
        }

        private void AttachAccountToContext(HttpContext context, string token, IUserAuthService authService)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(this.settings.JwtSecretKey);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userName = jwtToken.Claims.First(x => x.Type == Constants.ClaimName).Value;
                var userRole = (UserRole)int.Parse(jwtToken.Claims.First(x => x.Type == Constants.ClaimRole).Value);
                context.Items[Constants.UserAccountKey] = authService.IsUsernameAndRoleValid(userName, userRole);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}