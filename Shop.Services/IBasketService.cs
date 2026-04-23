using Shop.Entities;

namespace Shop.Services
{
    public interface IBasketService
    {
        int Add(int userId, int productId, int count);
        void Remove(int userId, int productId, int count);
        void RemoveAll(int userId, int productId);
        Basket Get(int userId);
    }
}