using System.Collections.Generic;

using Core.Repository;
using Domain.Models;

namespace Core.Services
{
    public class CartServices
    {
        private readonly ICartRepository _cardRepository;

        public CartServices(ICartRepository cardRepository)
            => this._cardRepository = cardRepository;

        public IEnumerable<Product> GetCartItems(IEnumerable<int> productIds)
            => this._cardRepository.GetCartItems(productIds);
    }
}
