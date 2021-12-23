using System.Collections.Generic;

using Core.Domain.Products;
using Domain.Models;

namespace Core.Domain.Cart
{
    public class CartServices
    {
        private readonly IProductRepository _productRepository;

        public CartServices(IProductRepository productRepository)
            => this._productRepository = productRepository;

        public IEnumerable<Product> GetCartItems(IEnumerable<int> productIds)
            => this._productRepository.GetByIds(productIds);
    }
}
