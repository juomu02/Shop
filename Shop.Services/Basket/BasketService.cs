using Shop.Entities;
using Shop.Repositories;

namespace Shop.Services
{
    public class BasketService : IBasketService
    {
        public IBasketRepository basketRepository;

        public BasketService(IBasketRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }

        public int Add(int userId, int productId, int count)
        {
            return basketRepository.Add(userId, productId, count);
        }
        public void Remove(int userId, int productId, int count)
        {
            basketRepository.Remove(userId, productId, count);
        }
        public void RemoveAll(int userId, int productId)
        {
            basketRepository.RemoveAll(userId, productId);
        }
        public Basket Get(int userId)
        {
            return basketRepository.Get(userId);
        }
    }
}