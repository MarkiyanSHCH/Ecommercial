using System.Collections.Generic;
using System.Linq;

namespace WebAPI.Models
{
    public class ProductList
    {
        public IEnumerable<Product> Products { get; set; }

        public ProductList()
            => this.Products = Enumerable.Empty<Product>();
    }
}