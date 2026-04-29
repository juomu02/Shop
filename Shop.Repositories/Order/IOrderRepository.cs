using Shop.Entities;

namespace Shop.Repositories
{
    public interface IOrderRepository
    {
        int Add(Basket basket);
        void Remove(int orderId);
        Order Get(int orderId);
        void ChangeStatus(int orderId, Order.IsPaid status);
    }
}