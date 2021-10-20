using System.Linq;

using Microsoft.AspNetCore.Mvc;

using WebAPI.Models;
using Core.Services;

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
            CategoryList categories = new CategoryList
            {
                Categories = _categoryServices.Get().ToList()
            };

            if (categories != null) return Ok(categories);

            return NotFound();
        }
    }
}