using System.Collections.Generic;
using System.Linq;

using Domain.Models;

namespace WebAPI.Models
{
    public class GetProductList
    {
        public IEnumerable<Product> Products { get; set; }

        public GetProductList()
            => this.Products = Enumerable.Empty<Product>();
    }
}