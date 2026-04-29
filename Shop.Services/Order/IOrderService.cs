using Shop.Entities;

namespace Shop.Services
{
    public interface IOrderService
    {

        int Add(Basket basket);
        void Remove(int orderId);
        Order Get(int orderId);
        void ChangeStatus(int orderId, Order.IsPaid status);
    }
}