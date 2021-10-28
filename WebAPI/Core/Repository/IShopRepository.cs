using System.Collections.Generic;

using Domain.Models;

namespace Core.Repository
{
    public interface IShopRepository
    {
        IEnumerable<Shop> GetAll();
        Shop GetById(int ShopId);
    }
}
