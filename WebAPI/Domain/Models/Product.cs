using System.Collections.Generic;
using System.Linq;

namespace Domain.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public string PhotoFileName { get; set; }
        public IEnumerable<Property> Properties { get; set; }

        public Product()
        {
            Properties = Enumerable.Empty<Property>();
        }
    }
}