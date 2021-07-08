using JeweleryShopApi.Entities;

namespace JeweleryShopApi.Services
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}