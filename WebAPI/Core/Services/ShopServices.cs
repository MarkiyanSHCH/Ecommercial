using System.Collections.Generic;

using Core.Repository;
using Domain.Models;

namespace Core.Services
{
    public class ShopServices
    {
        private readonly IShopRepository _shopRepository;

        public ShopServices(IShopRepository shopRepository)
            => this._shopRepository = shopRepository;

        public IEnumerable<Shop> GetAll()
            => this._shopRepository.GetAll();

        public Shop GetById(int ShopId)
            => this._shopRepository.GetById(ShopId);
    }
}
