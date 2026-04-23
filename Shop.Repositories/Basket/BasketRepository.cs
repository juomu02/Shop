using System.Data.Common;
using Shop.Data;
using Shop.Entities;

namespace Shop.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ShopDbContext dbContext;

        public BasketRepository(ShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public int Add(int userId, int productId, int count)
        {
            var basket = GetInstance(userId);
            AddBasketItem(basket.Id, productId, count);
            return basket.Id;
        }

        public void Remove(int userId, int productId, int count)
        {
            var basket = GetInstance(userId);
            RemoveBasketItem(basket.Id, productId, count);
        }

        public void RemoveAll(int userId, int productId)
        {
            var basket = GetInstance(userId);
            var product = dbContext.ProductInBaskets
                .FirstOrDefault(o => o.ProductId == productId && o.BasketId == basket.Id);
            dbContext.ProductInBaskets.Remove(product);
            dbContext.SaveChanges();
        }

        public Basket Get(int userId)
        {
            return dbContext.Baskets.SingleOrDefault(o => o.UserId == userId);
        }

        private Basket GetInstance(int userId)
        {
            var basket = dbContext.Baskets.SingleOrDefault(o => o.UserId == userId);
            if (basket != null) return basket;

            var newBasket = new Basket()
            {
                UserId = userId,
                ProductInBaskets = new List<ProductInBasket> { }
            };

            var entityEntry = dbContext.Baskets.Add(newBasket);

            dbContext.SaveChanges();

            return entityEntry.Entity;
        }

        private void AddBasketItem(int basketId, int productId, int count)
        {
            var product = dbContext.ProductInBaskets
                .FirstOrDefault(o => o.ProductId == productId && o.BasketId == basketId);
            if (product != null)
            {
                product.Count += count;
                dbContext.ProductInBaskets.Update(product);
                dbContext.SaveChanges();
            }

            else
            {
                var newProduct = new ProductInBasket
                {
                    BasketId = basketId,
                    ProductId = productId,
                    Count = count
                };
                dbContext.ProductInBaskets.Add(newProduct);
                dbContext.SaveChanges();
            }
        }

        private void RemoveBasketItem(int basketId, int productId, int count)
        {
            var product = dbContext.ProductInBaskets
                .FirstOrDefault(o => o.ProductId == productId && o.BasketId == basketId);
            if (product != null && product.Count > count)
            {
                product.Count -= count;
                dbContext.ProductInBaskets.Update(product);
                dbContext.SaveChanges();
            }
            else
            {
                dbContext.ProductInBaskets.Remove(product);
                dbContext.SaveChanges();
            }
        }
    }
}