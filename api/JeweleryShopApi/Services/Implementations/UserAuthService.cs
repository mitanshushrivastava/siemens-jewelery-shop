namespace JeweleryShopApi.Services
{
    using System.Linq;
    using System.Net;
    using JeweleryShopApi.Common.Enums;
    using JeweleryShopApi.Database;
    using JeweleryShopApi.Entities;
    using JeweleryShopApi.Helpers;
    using JeweleryShopApi.Models;

    public class UserAuthService : IUserAuthService
    {
        private readonly ITokenService tokenService;
        private readonly JeweleryShopDbContext dataContext;

        public UserAuthService(ITokenService tokenService, JeweleryShopDbContext dataContext)
        {
            this.tokenService = tokenService;
            this.dataContext = dataContext;
        }

        public string AuthenticateUser(AuthenticationRequest request)
        {
            var user = this.VerifyUsernameAndPassword(request);

            if(user != null && !string.IsNullOrEmpty(user.UserName))
            {
                return this.tokenService.GenerateToken(user);
            }

            return null;
        }

        private User VerifyUsernameAndPassword(AuthenticationRequest request)
        {
            var user = this.dataContext.Users.Where(x => x.UserName == request.UserName && x.Password == x.Password).FirstOrDefault();
            return user;
        }

        public User IsUsernameAndRoleValid(string userName, UserRole role)
        {
            var user = this.dataContext.Users.Where(x => x.UserName == userName && x.Role == role).FirstOrDefault();
            return user;
        }
    }
}