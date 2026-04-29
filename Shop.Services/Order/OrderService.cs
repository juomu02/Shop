using Shop.Entities;
using Shop.Repositories;

namespace Shop.Services
{
    public class OrderService : IOrderService
    {
        public IOrderRepository orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        public int Add(Basket basket)
        {
            return orderRepository.Add(basket);
        }
        public void Remove(int orderId)
        {
            orderRepository.Remove(orderId);
        }
        public Order Get(int orderId)
        {
            return orderRepository.Get(orderId);
        }
        public void ChangeStatus(int orderId, Order.IsPaid status)
        {
            orderRepository.ChangeStatus(orderId, status);
        }
    }
}