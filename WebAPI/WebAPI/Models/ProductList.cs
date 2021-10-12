using System.Collections.Generic;
using System.Linq;

using Domain.Models;

namespace WebAPI.Models
{
    public class ProductList
    {
        public IEnumerable<Product> Products { get; set; }

        public ProductList()
            => this.Products = Enumerable.Empty<Product>();
    }
}