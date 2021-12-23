using System.Collections.Generic;

using Domain.Models;

namespace Core.Domain.Products
{
    public class ProductServices
    {
        private readonly IProductRepository _productRepository;

        public ProductServices(IProductRepository productRepository)
            => this._productRepository = productRepository;

        public IEnumerable<Product> Get()
            => this._productRepository.GetAll();

        public Product GetById(int id)
            => this._productRepository.GetById(id);

        public IEnumerable<Product> GetByCategory(int id)
            => this._productRepository.GetByCategory(id);
    }
}