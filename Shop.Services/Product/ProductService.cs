using Shop.Entities;
using Shop.Repositories;

namespace Shop.Services
{
    public class ProductService : IProductService
    {
        public IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public int Add(Product product)
        {
            return productRepository.Add(product);
        }

        public Product Get(int id)
        {
            return productRepository.Get(id);
        }
    }
}