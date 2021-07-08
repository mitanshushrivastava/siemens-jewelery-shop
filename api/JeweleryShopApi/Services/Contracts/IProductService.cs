using JeweleryShopApi.Entities;

namespace JeweleryShopApi.Services
{
    public interface IProductService
    {
        public int GetDiscount(string productName);
    }
}