using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using JeweleryShopApi.Common.Enums;

namespace JeweleryShopApi.Entities
{
    public class User
    {
        [Key]
        public string UserName { get; set; }
        public UserRole Role { get; set; }
        
        [JsonIgnore]
        public string Password { get; set; }
    }
}