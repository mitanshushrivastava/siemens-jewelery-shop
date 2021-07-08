using System;
using System.Linq;
using JeweleryShopApi.Common.Enums;
using JeweleryShopApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JeweleryShopApi.Database
{
    public class InMemoryDatabaseGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using(var context = new JeweleryShopDbContext(serviceProvider.GetRequiredService<DbContextOptions<JeweleryShopDbContext>>()))
            {
                if (!context.Products.Any())
                {
                    context.Products.AddRange(
                        new Product
                        {
                            ProductId = 1,
                            ProductName = "Gold",
                            Discount = 2
                        }
                    );
                }

                if(!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User
                        {
                            Role = UserRole.NormalUser,
                            UserName = "NormalUser",
                            Password = "NormalUser"
                        },
                        new User
                        {
                            Role = UserRole.PrivilegedUser,
                            UserName = "PrivilegedUser",
                            Password = "PrivilegedUser"
                        }
                    );
                }

                context.SaveChanges();
            }
        }
    }
}