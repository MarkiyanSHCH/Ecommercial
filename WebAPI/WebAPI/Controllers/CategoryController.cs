using System.Linq;

using Microsoft.AspNetCore.Mvc;

using Core.Domain.Categories;

using WebAPI.Models.Api.Categories;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryServices _categoryServices;

        public CategoryController(CategoryServices categoryServices)
            => this._categoryServices = categoryServices;

        [HttpGet]
        public IActionResult Get()
        {
            var categories = new GetCategoryList
            {
                Categories = this._categoryServices.Get().ToList()
            };

            if (categories.Categories.Any()) 
                return Ok(categories);

            return NotFound();
        }
    }
}