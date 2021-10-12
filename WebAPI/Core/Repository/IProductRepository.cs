using System.Collections.Generic;

using Domain.Models;

namespace Core.Repository
{
    public interface IProductRepository
    {
        public IEnumerable<Product> GetAll();
        public IEnumerable<Product> GetByCategory(int Id);
        public Product GetById(int Id);
    }
}