using System.Collections.Generic;

using Domain.Models;

namespace Core.Repository
{
    public interface IOrderRepository
    {
        public IEnumerable<Product> GetAll(string Id);
    }
}