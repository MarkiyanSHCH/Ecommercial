using System.Collections.Generic;
using System.Linq;

namespace WebAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }
        public string PhotoFileName { get; set; }

        public IEnumerable<Characteristics> characteristics { get; set; }

        public Product()
        {
            characteristics = Enumerable.Empty<Characteristics>();
        }
    }
}
