using Shop.Data;
using Shop.Entities;

namespace Shop.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShopDbContext dbContext;
        public OrderRepository(ShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public int Add(Basket basket)
        {
            var order = new Order
            {
                UserId = basket.UserId,
                Status = 0
            };

            var entityEntry = dbContext.Orders.Add(order);

            dbContext.SaveChanges();

            var orderId = entityEntry.Entity.Id;

            CopyFromBasket(orderId, basket);

            return orderId;
        }

        public void Remove(int orderId)
        {
            var order = Get(orderId);
            dbContext.Orders.Remove(order);
            dbContext.SaveChanges();
        }

        public Order Get(int orderId)
        {
            return dbContext.Orders.FirstOrDefault(o => o.Id == orderId);
        }

        public void ChangeStatus(int orderId, Order.IsPaid status)
        {
            var order = Get(orderId);
            order.Status = status;
            dbContext.Orders.Update(order);
            dbContext.SaveChanges();
        }

        private void CopyFromBasket(int orderId, Basket basket)
        {
            foreach (var basketItem in basket.ProductInBaskets)
            {
                var newProductInOrder = new ProductInOrder
                {
                    OrderId = orderId,
                    ProductId = basketItem.ProductId,
                    Count = basketItem.Count
                };

                dbContext.ProductInOrders.Add(newProductInOrder);
                dbContext.ProductInBaskets.Remove(basketItem);
            }
            dbContext.SaveChanges();
        }
    }
}