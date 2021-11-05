using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Domain.Models;

namespace WebAPI.Models
{
    public class PostOrderRequest
    {
        [Required]
        public int ShopId { get; set; }
        [Required]
        public double TotalPrice { get; set; }
        public IEnumerable<OrderLine> OrderLines { get; set; }

        public PostOrderRequest()
            => this.OrderLines = Enumerable.Empty<OrderLine>();
    }
}