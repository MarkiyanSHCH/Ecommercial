using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public readonly CategoryServices _categoryServices;

        public CategoryController(IConfiguration configuration)
        {
            _configuration = configuration;
            _categoryServices = new CategoryServices();
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Category> products = _categoryServices.Get(_configuration);

            if (products != null)
                return Ok(products);

            return NotFound();
        }
    }
}
