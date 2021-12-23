using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WebAPI.Models.Api.Orders
{
    public class PostOrderRequest
    {
        [Required]
        public int ShopId { get; set; }
        [Required]
        public double TotalPrice { get; set; }
        public IEnumerable<PostOrderLineRequest> OrderLines { get; set; }

        public PostOrderRequest()
            => this.OrderLines = Enumerable.Empty<PostOrderLineRequest>();
    }
}