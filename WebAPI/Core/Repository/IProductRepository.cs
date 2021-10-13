using System.Collections.Generic;

using Domain.Models;

namespace Core.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetByCategory(int Id);
        Product GetById(int Id);
    }
}