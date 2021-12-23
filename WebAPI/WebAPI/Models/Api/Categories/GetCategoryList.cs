using System.Collections.Generic;
using System.Linq;

using Domain.Models;

namespace WebAPI.Models.Api.Categories
{
    public class GetCategoryList
    {
        public IEnumerable<Category> Categories { get; set; }

        public GetCategoryList()
            => this.Categories = Enumerable.Empty<Category>();
    }
}