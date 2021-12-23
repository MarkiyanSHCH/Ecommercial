using System.Collections.Generic;

using Domain.Models;

namespace Core.Domain.Products
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(int productId);
        IEnumerable<Product> GetByCategory(int categoryId);
        IEnumerable<Product> GetByIds(IEnumerable<int> productIds);
    }
}