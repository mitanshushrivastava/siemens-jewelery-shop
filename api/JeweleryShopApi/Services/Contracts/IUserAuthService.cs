using JeweleryShopApi.Common.Enums;
using JeweleryShopApi.Entities;
using JeweleryShopApi.Models;

namespace JeweleryShopApi.Services
{
    public interface IUserAuthService
    {
        public string AuthenticateUser(AuthenticationRequest request);
        public User IsUsernameAndRoleValid(string userName, UserRole role);
    }
}