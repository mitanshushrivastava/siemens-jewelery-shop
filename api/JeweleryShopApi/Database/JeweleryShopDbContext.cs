using JeweleryShopApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace JeweleryShopApi.Database
{
    public class JeweleryShopDbContext : DbContext
    {
        public JeweleryShopDbContext(DbContextOptions<JeweleryShopDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}