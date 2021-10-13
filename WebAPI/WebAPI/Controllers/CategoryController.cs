using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using Domain.Models;
using Core.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public readonly CategoryServices _categoryServices;

        public CategoryController(CategoryServices categoryServices)
            => this._categoryServices = categoryServices;

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Category> products = _categoryServices.Get();

            if (products != null)
                return Ok(products);

            return NotFound();
        }
    }
}