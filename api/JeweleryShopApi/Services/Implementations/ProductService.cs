using System.Linq;
using JeweleryShopApi.Database;
using JeweleryShopApi.Entities;

namespace JeweleryShopApi.Services
{
    public class ProductService : IProductService
    {
        private readonly JeweleryShopDbContext dataContext;
        public ProductService(JeweleryShopDbContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public int GetDiscount(string productName)
        {
            var product = this.dataContext.Products.First(x => string.Equals(x.ProductName, productName, System.StringComparison.OrdinalIgnoreCase));
            if (product != null)
            {
                return product.Discount;
            }
            
            return -1;
        }
    }
}