using System.Collections.Generic;

using Domain.Models;

namespace Core.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(int Id);
        IEnumerable<Product> GetByCategory(int Id);
    }
}