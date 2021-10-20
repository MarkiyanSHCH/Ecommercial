using System.Collections.Generic;

using Domain.Models;

namespace Core.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<Product> GetAll(int Id);
        public bool AddOrderProduct(int UserId, int ProductId);
        public bool RemoveOrderProduct(int UserId, int ProductId);
    }
}