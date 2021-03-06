using System.Collections.Generic;
using System.Linq;

namespace WebAPI.Models.Api.Cart
{
    public class GetCartItemsId
    {
        public IEnumerable<int> ProductIds { get; set; }

        public GetCartItemsId()
            => this.ProductIds = Enumerable.Empty<int>();
    }
}
