using System.Collections.Generic;
using System.Linq;

using Domain.Models;

namespace WebAPI.Models.Api.Orders
{
    public class GetOrderLinesList
    {
        public IEnumerable<OrderLine> OrderLines { get; set; }

        public GetOrderLinesList()
            => this.OrderLines = Enumerable.Empty<OrderLine>();
    }
}
