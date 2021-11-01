using System.Collections.Generic;

using Domain.Models;

namespace Core.Repository
{
    public interface ICartRepository
    {
        IEnumerable<Product> GetCartItems(IEnumerable<int> productIds);
    }
}