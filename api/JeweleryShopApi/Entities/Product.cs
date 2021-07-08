using System.ComponentModel.DataAnnotations;

namespace JeweleryShopApi.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Discount { get; set; }
    }
}