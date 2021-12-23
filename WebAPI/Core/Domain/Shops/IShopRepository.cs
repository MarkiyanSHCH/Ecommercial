using System.Collections.Generic;

using Domain.Models;

namespace Core.Domain.Shops
{
    public interface IShopRepository
    {
        IEnumerable<Shop> GetAll();
        Shop GetById(int shopId);
    }
}
