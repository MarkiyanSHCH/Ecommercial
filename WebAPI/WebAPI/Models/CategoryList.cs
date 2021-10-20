using System.Collections.Generic;
using System.Linq;

using Domain.Models;

namespace WebAPI.Models
{
    public class CategoryList
    {
        public IEnumerable<Category> Categories { get; set; }

        public CategoryList()
            => this.Categories = Enumerable.Empty<Category>();
    }
}