using System.Collections.Generic;
using System.Linq;

using Domain.Models;

namespace Core.Domain.Orders.Models
{
    public class OrderConfirmationNotification
    {
        public int OrderNumber { get; set; }
        public double TotalPrice { get; set; }
        public string ShippingAddress { get; set; }
        public IEnumerable<OrderLine> OrderList { get; set; }

        public OrderConfirmationNotification()
            => this.OrderList = Enumerable.Empty<OrderLine>();
    }
}
