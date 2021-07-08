namespace JeweleryShopApi.Models
{
    public class InvoiceRequest
    {
        public float PricePerGram { get; set; }
        public float Weight { get; set; }
        public int Discount { get; set; }
        public string CustomerName { get; set; }
    }
}