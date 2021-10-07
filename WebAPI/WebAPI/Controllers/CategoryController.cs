using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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
            return Ok(_categoryServices.Get(_configuration));
        }

    }
}
