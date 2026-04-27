using Shop.Entities;

namespace Shop.Repositories
{
    public interface IAccountRepository
    {
        int Add(int userId, int productId, int count);
        void Remove(int userId, int productId, int count);
        Basket Get(int userId);
    }
}