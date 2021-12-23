using System.Collections.Generic;

using Domain.Models;

namespace Core.Domain.Shops
{
    public class ShopServices
    {
        private readonly IShopRepository _shopRepository;

        public ShopServices(IShopRepository shopRepository)
            => this._shopRepository = shopRepository;

        public IEnumerable<Shop> GetAll()
            => this._shopRepository.GetAll();

        public Shop GetById(int shopId)
            => this._shopRepository.GetById(shopId);
    }
}
