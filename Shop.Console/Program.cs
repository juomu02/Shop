using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop.Data;
using Shop.Entities;
using Shop.Repositories;
using Shop.Services;

namespace Shop.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = BuildHost();

            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                var dbContext = serviceProvider.GetRequiredService<ShopDbContext>();
                dbContext.Database.Migrate();

                var productService = serviceProvider.GetRequiredService<IProductService>();

                var id = productService.Add(new Product()
                {
                    Name = "Book2",
                    Price = 2.99M
                });

                var product = productService.Get(id);

                Console.WriteLine($"id: {product.Id}; name: {product.Name}");

                //Uzdaviniai 1-7 test
                var userId = 1;
                var basketService = serviceProvider.GetRequiredService<IBasketService>();
                basketService.Add(userId, product.Id, 5);
                basketService.Add(userId, product.Id, 9);
                basketService.Add(userId, 1, 1);
                basketService.Add(userId, 1, 1);
                basketService.Remove(userId, 1, 1);
                basketService.Add(userId, 2, 3);
                basketService.RemoveAll(userId, 2);
                basketService.Add(userId, 2, 5);

                var userBasket = basketService.Get(userId);
                for (int productIndex = 0; productIndex < userBasket.Products.Count; productIndex++)
                {
                    Console.WriteLine($"User {userId} has item {userBasket.Products[productIndex].ProductId} with count {userBasket.Products[productIndex].Count}");
                }
            }
        }

        public static IHost BuildHost()
        {
            var host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
            {
                var connectionString = context.Configuration.GetConnectionString("DefaultConnection");

                services.AddDbContext<ShopDbContext>(options =>
                {
                    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                });

                services.AddScoped<IProductRepository, ProductRepository>();
                services.AddScoped<IBasketRepository, BasketRepository>();
                services.AddScoped<IBasketService, BasketService>();
                services.AddScoped<IProductService, ProductService>();
            });

            return host.Build();
        }

    }
}