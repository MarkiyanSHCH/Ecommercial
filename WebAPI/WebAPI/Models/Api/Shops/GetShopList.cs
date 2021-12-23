using System.Collections.Generic;
using System.Linq;

using Domain.Models;

namespace WebAPI.Models.Api.Shops
{
    public class GetShopList
    {
        public IEnumerable<Shop> Shops { get; set; }

        public GetShopList()
            => this.Shops = Enumerable.Empty<Shop>();
    }
}