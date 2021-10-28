using System.Collections.Generic;
using System.Linq;

using Domain.Models;

namespace WebAPI.Models
{
    public class PostOrderRequest
    {
        public int ShopId { get; set; }
        public double TotalPrice { get; set; }
        public IEnumerable<OrderLine> OrderLines { get; set; }

        public PostOrderRequest()
            => this.OrderLines = Enumerable.Empty<OrderLine>();
    }
}