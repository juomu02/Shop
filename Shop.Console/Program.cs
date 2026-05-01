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

                // paskaitos #21 uždavinių test
                var userService = serviceProvider.GetRequiredService<IUserService>();

                var newUserName = "Ignas";
                var newUserPwd = "215as-fg2*";


                var getUser = userService.Get(newUserName);
                if (getUser != null) userService.Remove(getUser.Id);

                var newUserId = userService.Add(newUserName, newUserPwd);
                Console.WriteLine($"Sukurtas naujas useris su id: {newUserId}");

                getUser = userService.Get(newUserName);
                Console.WriteLine($"Id: {getUser.Id}, name:{getUser.UserName}, encrypted password: {getUser.Password}");

                var wrongPassword = "blogasSlaptazodis";
                var wrongPwdCheck = userService.CheckPassword(getUser.Id, wrongPassword);
                Console.WriteLine($"Blogo password check:{wrongPwdCheck}");

                var correctPwdCheck = userService.CheckPassword(getUser.Id, newUserPwd);
                Console.WriteLine($"Gero password check:{correctPwdCheck}");

                userService.ChangePassword(getUser.Id, wrongPassword);
                userService.GetAll();

                var changedPwdCheck = userService.CheckPassword(getUser.Id, wrongPassword);
                Console.WriteLine($"Pakeisto password check:{changedPwdCheck}");


                // IProductService test
                var productService = serviceProvider.GetRequiredService<IProductService>();

                var id = productService.Add(new Product()
                {
                    Name = "T-Shirt",
                    Price = 12.99M
                });

                var product = productService.Get(id);

                Console.WriteLine($"id: {product.Id}; name: {product.Name}");


                // paskaitos #18 uždavinių test
                var userId = newUserId != null ? (int)newUserId : 1;

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
                var userBasketList = userBasket.ProductInBaskets.ToList();
                // for (int productIndex = 0; productIndex < userBasket.ProductInBaskets.Count; productIndex++)
                // {
                //     Console.WriteLine($"User {userId} has item {userBasketList[productIndex].ProductId} with count {userBasketList[productIndex].Count}");
                // }

                // paskaitos #22 uždavinių test
                var orderService = serviceProvider.GetRequiredService<IOrderService>();
                var newOrderId = orderService.Add(userBasket);
                var newOrder = orderService.Get(newOrderId);
                orderService.ChangeStatus(newOrder.Id, (Order.IsPaid)2);
                var orderItemsList = newOrder.ProductInOrders.ToList();
                for (int productIndex = 0; productIndex < newOrder.ProductInOrders.Count; productIndex++)
                {
                    Console.WriteLine($"User {userId} has item {orderItemsList[productIndex].ProductId} with count {orderItemsList[productIndex].Count}");
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

                AddPersistentServices(services);
            });

            return host.Build();
        }

        private static void AddPersistentServices(IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();
        }

    }
}