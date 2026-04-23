using Shop.Entities;

namespace Shop.Services
{
    public interface IProductService
    {
        int Add(Product product);
        Product Get(int id);
    }
}