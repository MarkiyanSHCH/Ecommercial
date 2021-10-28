using System.Collections.Generic;
using System.Linq;

using Domain.Models;

namespace WebAPI.Models
{
    public class GetOrdersList
    {
        public IEnumerable<Order> Orders { get; set; }

        public GetOrdersList()
            => this.Orders = Enumerable.Empty<Order>();
    }
}
